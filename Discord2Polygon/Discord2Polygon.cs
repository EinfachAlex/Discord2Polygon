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
}