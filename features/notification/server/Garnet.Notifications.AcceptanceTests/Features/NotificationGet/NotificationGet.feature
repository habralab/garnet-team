Функция: 
    Я, как пользователь 
    Хочу иметь возможность получать уведомления о приглашение в команду
    Чтобы быть вовремя осведомленным

        Сценарий: Уведомление о приглашении в команду
            Допустим существует пользователь 'Петя'
            Когда появляется приглашение пользователя 'Петя' в команду 'FooBar'
            Тогда пользователь 'Петя' получает уведомление типа 'TeamInvite' с названием 'Приглашение в команду' и текстом 'Вас пригласили вступить в команду FooBar'