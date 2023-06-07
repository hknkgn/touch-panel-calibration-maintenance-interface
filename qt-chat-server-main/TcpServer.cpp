#include "TcpServer.hpp"

TcpServer::TcpServer(QObject *parent) : QObject(parent)
{
    connect(&_server, &QTcpServer::newConnection, this, &TcpServer::onNewConnection);
    connect(this, &TcpServer::newMessage, this, &TcpServer::onNewMessage);
    if(_server.listen(QHostAddress::Any, 45000)) {
        qInfo() << "İstemci Bekleniyor ...";
    }
}

void TcpServer::onNewConnection()
{
    const auto client = _server.nextPendingConnection();
    if(client == nullptr) {
        return;
    }

    qInfo() << "Yeni İstemci Bağlı.";

    //_clients.insert(this->getClientKey(client), client);

    connect(client, &QTcpSocket::readyRead, this, &TcpServer::onReadyRead);
    connect(client, &QTcpSocket::disconnected, this, &TcpServer::onClientDisconnected);
}

void TcpServer::onReadyRead()
{
    const auto client = qobject_cast<QTcpSocket*>(sender());

    if(client == nullptr) {
        return;
    }

    const auto message = "İstemci: " + client->readAll();

    emit newMessage(message);
}

void TcpServer::onClientDisconnected()
{
    const auto client = qobject_cast<QTcpSocket*>(sender());

    if(client == nullptr) {
        return;
    }
    const auto message = "İstemci Ayrıldı";
    emit newMessage(message);
    qInfo() << "İstemci Ayrıldı";
    //_clients.remove(this->getClientKey(client));
}

void TcpServer::onNewMessage(const QByteArray &ba)
{
    for(const auto &client : qAsConst(_clients)) {
        client->write(ba);
        client->flush();
    }
}

/*QString TcpServer::getClientKey(const QTcpSocket *client) const
{
    return client->peerAddress().toString().append(":").append(QString::number(client->peerPort()));
}*/
