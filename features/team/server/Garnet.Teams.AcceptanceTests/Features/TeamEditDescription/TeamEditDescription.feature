Функция: TeamEditDescription
    Я, как пользователь-владелец команды
    Хочу иметь возможность редактировать параметры своей команды
    Чтобы поддерживать профиль команды в актуальном состоянии

        Сценарий: Редактирование описания карточки команды
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И описание команды 'FooBar' состоит из 'DreamTeam'
            Когда 'Вася' редактирует описание карточки команды 'FooBar' на 'NewDreamTeam'
            Тогда описание команды в карточке состоит из 'NewDreamTeam'

        Сценарий: Редактирование карточки команды запрещено не владельцем команды
            Допустим существует пользователь 'Вася'
            И существует пользователь 'Маша'
            И существует команда 'FooBar'
            И владельцем команды 'FooBar' является 'Вася'
            И описание команды 'FooBar' состоит из 'DreamTeam'
            Когда 'Маша' редактирует описание карточки команды 'FooBar' на 'NewDreamTeam'
            Тогда пользователь получает ошибку 'TeamOnlyOwnerCanChangeDescriptionError'
            И описание команды 'FooBar' состоит из 'DreamTeam'