import QtQuick 2.12
import QtQuick.Window 2.12
import QtQuick.Controls 2.12
import QtQuick.Layouts 1.12

Window {
    width: 640
    height: 480
    visible: true
    title: qsTr("Chat Server")
    color: "gray"
    Connections {
        target: server
        function onNewMessage(ba) {
            listModelMessages.append({
                                         message: ba + ""
                                     })
        }
    }

    ColumnLayout {
        anchors.fill: parent
        ListView {
            Layout.fillHeight: true
            Layout.fillWidth: true
            spacing: 20

            clip: true
                model: ListModel {
                    id: listModelMessages
                    ListElement {
                        message: "İstemciden Mesaj Bekleniyor..."
                    }
                }
                delegate: ItemDelegate {

                    background:
                        Rectangle {
                            color:"gray"
                            Text {
                                id: name
                                padding: 5
                                text: message
                                color:"black"
                            }
                        }
                }


//            model: ListModel {
//                id: listModelMessages
//                ListElement {
//                    message: "İstemciden Mesaj Bekleniyor..."
//                }
//            }
//            delegate: ItemDelegate {
//                Text {
//                    id: name
//                    text: message
//                    color:"red"
//                }
//            }
            ScrollBar.vertical: ScrollBar {}
        }
        RowLayout {
            Layout.fillWidth: true
            TextField {
                id: textFieldMessage
                placeholderText: qsTr("SUNUCU BAŞLATILDI......")
                Layout.fillWidth: true
            }
        }
    }
}
