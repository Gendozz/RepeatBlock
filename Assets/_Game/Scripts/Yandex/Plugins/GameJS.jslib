mergeInto(LibraryManager.library, {

    ShowAdv: function () {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onOpen: function () {
                    console.log('Ad opened succesfully');
                    myGameInstance.SendMessage('Yandex', 'OpenAd'); // Успешное открытие
                },
                onClose: function (wasShown) {
                    if (wasShown) {
                        console.log('Ad was shown and closed');
                        myGameInstance.SendMessage('Yandex', 'CloseAd'); // Была показана и закрыта
                    } else {
                        console.log('Ad wasnt shown and closed');
                        myGameInstance.SendMessage('Yandex', 'CloseAd'); // Не была показана
                    }
                },
                onError: function (error) {
                    console.log('Error when showing add');
                    myGameInstance.SendMessage('Yandex', 'CloseAd'); // Ошибка при показе
                }
            }
        })
    },


    PrintToConsole: function (someText) {
        console.log('PTC ' + UTF8ToString(someText));
    }

	//Pause: function () {
 //       myGameInstance.SendMessage('Yandex', 'Pause');
 //       console.log('PTC  - Sent Pause to Yandex');
 //   },

 //   Play: function () {
 //       myGameInstance.SendMessage('Yandex', 'Play');
 //       console.log('PTC  - Sent Play to Yandex');
 //   }
});