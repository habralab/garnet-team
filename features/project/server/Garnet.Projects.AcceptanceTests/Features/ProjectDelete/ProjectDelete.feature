﻿Функция:
Я, как пользователь-владелец проекта
Хочу иметь возможность удалить свой проект
Если я считаю, что работа над проектом завершена

    Сценарий: Удаление проекта
        Допустим существует пользователь 'Вася'
        И существует проект 'Dummy' с владельцем 'Вася'
        Когда 'Вася' удаляет проект 'Dummy'
        Тогда проекта 'Dummy' в системе не существует

    Сценарий: Удалить проект способен только владелец проекта
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Маша'
        И существует проект 'Dummy' с владельцем 'Вася'
        Когда 'Маша' удаляет проект 'Dummy'
        Тогда пользователь получает ошибку, что 'ProjectOnlyOwnerCanDeleteError'
        И в системе присутствует проект 'Dummy'