Функция: 
    Я, как пользователь
    Хочу иметь возможность создавать посты в ленте своей команды
    Чтобы уведомлять других пользователей о новостях от имени команды

    Контекст:
        Допустим существует команда 'DreamTeam'
        И существует пользователь 'Вася'
        И пользователь 'Вася' является участником команды 'DreamTeam'

    Сценарий: Создание поста
        Когда пользователь 'Вася' создает пост с содержанием 'Супер мега анонс' в ленте команды 'DreamTeam'
        Тогда количество постов в ленте команды 'DreamTeam' равно '1'

    Сценарий: Только участники могут создавать посты в ленте команды
        Допустим существует пользователь 'Маша'
        Когда пользователь 'Маша' создает пост с содержанием 'Супер мега анонс' в ленте команды 'DreamTeam'
        Тогда пользователь получает ошибку 'Создавать посты в ленте команды могут только ее участники'