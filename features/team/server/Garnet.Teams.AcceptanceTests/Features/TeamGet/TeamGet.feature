Функция:
Я, как пользователь 
Хочу иметь возможность просмотреть карточку команды
Чтобы прочитать о ней необходимую мне информацию

    Сценарий: Просмотр карточки команды
        Допустим существует пользователь 'Вася'
        Допустим существует команда 'FooBar'
        И описание команды 'FooBar' состоит из 'DreamTeam'
        Когда 'Вася' открывает карточку команды 'FooBar'
        Тогда описание команды в карточке состоит из 'DreamTeam'
        И имя команды в карточке состоит из 'FooBar'

    Сценарий: Просмотр карточки несуществующей команды
        Допустим существует пользователь 'Вася'
        Когда 'Вася' открывает карточку команды, которой нет в системе
        Тогда пользователь получает ошибку 'TeamNotFoundError'