Функция: 
    Я, как пользователь-владелец команды
    Хочу иметь возможность редактировать теги своей команды
    Чтобы изменить требуемые навыки для вступления пользователей

    Контекст:
            Допустим существует пользователь 'Вася'
            И существует команда 'FooBar'
            И теги команды 'FooBar' состоят из 'marketing, smm'

        Сценарий: Редактирование тегов команды
            Допустим владельцем команды 'FooBar' является 'Вася'
            Когда 'Вася' редактирует теги команды 'FooBar' на 'development, ui, ux'
            Тогда теги команды 'FooBar' состоят из 'development, ui, ux'

        Сценарий: Редактирование тегов команды запрещено не владельцу команды
            Когда 'Вася' редактирует теги команды 'FooBar' на 'development, ui, ux'
            Тогда пользователь получает ошибку 'TeamOnlyOwnerCanChangeTagsError'
            И теги команды 'FooBar' состоят из 'marketing, smm'