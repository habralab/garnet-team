﻿Функция:
Я, как пользователь-владелец проекта
Хочу иметь возможность сменить владельца своего проекта
Чтобы передать функционал по управлению проектом в другие руки

    Сценарий: Передача владельца проекта
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Маша'
        И существует проект 'Dummy' с владельцем 'Вася'
        Когда 'Вася' изменяет владельца проекта 'Dummy' на пользователя 'Маша'
        Тогда владельцем проекта 'Dummy' является  'Маша'
        И для пользователя 'Маша' существует уведомление типа 'ProjectEditOwner'
        И в последнем уведомлении для пользователя 'Маша' связанной сущностью является проект 'Dummy'

    Сценарий: Передача владельца проекта возможна только владельцем проекта
        Допустим существует пользователь 'Вася'
        И существует пользователь 'Маша'
        И существует проект 'Dummy' с владельцем 'Вася'
        Когда 'Маша' изменяет владельца проекта 'Dummy' на пользователя 'Маша'
        Тогда пользователь получает ошибку, что 'ProjectOnlyOwnerCanEditError'
        И владельцем проекта 'Dummy' является  'Вася'