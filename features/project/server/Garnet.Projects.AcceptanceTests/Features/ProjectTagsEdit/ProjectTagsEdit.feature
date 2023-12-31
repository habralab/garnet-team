﻿Функция:
Я, как пользователь-владелец проекта
Хочу иметь возможность редактировать теги своего проекта
Чтобы изменить технологии применяемые в проекте

    Контекст:
        Допустим существует пользователь 'Вася'
        И существует проект 'FooBar' с владельцем 'Вася'
        И теги проекта 'FooBar' состоят из 'C#, React'

    Сценарий: Редактирование тегов проекта
        Когда 'Вася' редактирует теги проекта 'FooBar' на 'Python, SQL'
        Тогда теги проекта 'FooBar' состоят из 'Python, SQL'

    Сценарий: Редактирование тегов проекта запрещено не владельцу проекта
        Допустим  существует пользователь 'Маша'
        Когда 'Маша' редактирует теги проекта 'FooBar' на 'Python, SQL'
        Тогда пользователь получает ошибку, что 'ProjectOnlyOwnerCanEditError'
        И теги проекта 'FooBar' состоят из 'C#, React'