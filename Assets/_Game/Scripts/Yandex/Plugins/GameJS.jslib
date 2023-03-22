mergeInto(LibraryManager.library, {
  
  ShowAdv : function () {
	ysdk.adv.showFullscreenAdv({
		callbacks: {	
			onOpen: function() {
				console.log('Ad opened succesfully');
				myGameInstance.SendMessage('Yandex', 'OpenAd'); // �������� ��������
			},			
			onClose: function (wasShown) {
				if (wasShown) {
					console.log('Ad was shown and closed');
					myGameInstance.SendMessage('Yandex', 'CloseAd'); // ���� �������� � �������
				} else {
					console.log('Ad wasnt shown and closed');
					myGameInstance.SendMessage('Yandex', 'CloseAd'); // �� ���� ��������
				}				
			},
			onError: function(error) {
				console.log('Error when showing add');
				myGameInstance.SendMessage('Yandex', 'CloseAd'); // ������ ��� ������
			}
		}
	})
  },
  
  PrintToConsole: function (someText) {
    console.log('PTC ' + UTF8ToString(someText));
  }

});