Функция: 
    Я, как пользователь 
    Хочу иметь возможность смотреть свои команды и команды других пользователей
    Чтобы увидеть количество сообществ, в которых я или пользователь являемся участниками

        Сценарий: Пользователь смотрит список команд
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И пользователь 'Вася' является участником команды 'FooBar'
            И существует команда 'DreamTeam'
            И владельцем команды 'DreamTeam' является 'Вася'
            И существует команда 'Dummy'
            Когда производится запрос списка команд пользователя 'Вася'
            Тогда количество команд пользователя в результате равно '2'