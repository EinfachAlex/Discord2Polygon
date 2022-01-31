using System.Numerics;
using EinfachAlex.Utils.Logging;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Account = Nethereum.Web3.Accounts.Account;

namespace Discord2Polygon;

public class Discord2Polygon : IDiscord2Polygon
{
    private string privateKey;
    private readonly int chainID;
    private readonly string web3URL;

    private Web3 web3;
    private Account account;
    private IContractTransactionHandler<ReceiveFromPolygonDTO> receiveFromPolygonContractMethod;
    private IContractTransactionHandler<SendToPolygonDTO> sendToPolygonContractMethod;

    private readonly ContractInfo contractInfo;

    public Discord2Polygon(string privateKey, int chainID, string web3URL, ContractInfo contractInfo)
    {
        this.privateKey = privateKey;
        this.chainID = chainID;
        this.web3URL = web3URL;
        this.contractInfo = contractInfo;

        init();
    }

    private void init()
    {
        this.account = loginWithPrivateKey();

        this.web3 = connectToWeb3(this.account, this.web3URL);
        
        //this.receiveFromPolygonContractMethod = web3.Eth.GetContractTransactionHandler<ReceiveFromPolygonDTO>();
        this.sendToPolygonContractMethod = web3.Eth.GetContractTransactionHandler<SendToPolygonDTO>();
    }

    private Web3 connectToWeb3(Account account, string web3URL)
    {
        Logger.i($"Connecting to web3 - URL {web3URL}");
        
        Web3 web3 = new Web3(account, web3URL);

        Logger.i("Connected to Web3!");

        web3.TransactionManager.UseLegacyAsDefault = true;

        return web3;
    }

    private Account loginWithPrivateKey()
    {
        Logger.i($"Logging in to chainID {chainID}");

        Account account = new Account(this.privateKey, this.chainID);
        
        Logger.v($"=> Using ETH-Address {account.Address}");
        
        return account;
    }

    public async Task<TransactionReceipt> sendToPolygon(SendToPolygonDTO sendToPolygonDTO)
    {
        HexBigInteger estimatedGasLimit = await estimateGas(sendToPolygonDTO);
        
        sendToPolygonDTO.Gas = estimatedGasLimit;
        
        return await sendTransactionToBlockchain(sendToPolygonDTO);
    }
    
    private async Task<HexBigInteger> estimateGas(SendToPolygonDTO sendToPolygonDTO)
    {
        Logger.i("Estimating gas...");

        HexBigInteger estimateGas = await this.sendToPolygonContractMethod.EstimateGasAsync(this.contractInfo.address, sendToPolygonDTO);

        Logger.v($"=> estimated {estimateGas} gas");
        
        return estimateGas;
    }

    private async Task<TransactionReceipt> sendTransactionToBlockchain(SendToPolygonDTO receiveFromPolygonDto)
    {
        Logger.i("Creating transaction...");
        
        TransactionReceipt transaction = await this.sendToPolygonContractMethod.SendRequestAndWaitForReceiptAsync(this.contractInfo.address, receiveFromPolygonDto);

        Logger.i($"=> Sent transaction {transaction.TransactionHash}");

        return transaction;
    }

    public async Task checkForNewEvents(BigInteger sinceBlock)
    {
        Logger.i($"Checking for events after block {sinceBlock}");
        
        Contract contract = web3.Eth.GetContract(this.contractInfo.abi, this.contractInfo.address);

        Event? multiplyEvent = contract.GetEvent("receiveFromPolygonEvent");
        HexBigInteger? filterAll = await multiplyEvent.CreateFilterAsync(new BlockParameter((ulong)sinceBlock + 1));
        
        List<EventLog<ReceiveFromPolygonDTO>> eventLogs = await multiplyEvent.GetAllChangesAsync<ReceiveFromPolygonDTO>(filterAll);
        
        Logger.i($"Found {eventLogs.Count} new events!");
        
        foreach (EventLog<ReceiveFromPolygonDTO> eventLog in eventLogs)
        {
            EventHandler<ReceiveFromPolygonDTO> eventHandler = receiveFromPolygon;
            eventHandler?.Invoke(this, eventLog.Event);
        }
    }
    
    public Task<HexBigInteger> latestBlockNumber => web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();


    public event EventHandler<ReceiveFromPolygonDTO> receiveFromPolygon;
}