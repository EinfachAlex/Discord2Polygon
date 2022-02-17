# Discord2Polygon

## Das Problem
Aktuell sind Discord-Bots wenig bis gar nicht miteinander verbunden. 
Die meisten Bots implementieren ihr eigenes Finanzsystem. Diese sind untereinander nicht kompatibel.

Aus diesem Grund müsste für jede Bot-zu-Bot Verbindung ein eigenes Programm geschrieben werden, das die Daten der jeweiligen Bots übersetzt.
Um die Verbindung zwischen gerade einmal sechs Bots zu ermöglichen, wären schon 21 verschiedene Programme nötig.

## Die Lösung
Discord2Polygon löst dieses Problem, indem die jeweiligen Währungen der Bots an die Polygon-Blockchain angebunden werden.
Dort wird von allen Coins der ERC20-Standard implementiert, was einen einfachen Austausch der Coins und eine Interaktion mit anderen Apps auf der Blockchain ermöglicht.

Jeder Bot implementiert die Funktionen der Discord2Polygon-Bibliothek um mit der Blockchain zu interagieren.
Die Währung des jeweiligen Bots kann von Discord an die Blockchain geschickt werden.

### Beispiel: 
Nutzer A besitzt 100 Coins bei Bot B. 
50 seiner Coins schickt er über eine Funktion im Bot B an die Blockchain.

Nutzer A besitzt nun zwar immer noch 100 Coins. Davon sind jeweils 50 auf Discord und 50 auf der Blockchain.

Die Coins auf der Blockchain kann Nutzer A nun verwenden, um dort mit verschiedenen dApps zu interagieren.
Beispielsweise können die Coins bei dezentralisierten Handelsplattformen gegen die Coins von anderen Bots getauscht werden.