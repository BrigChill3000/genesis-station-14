# proper handling for having a min/max or not
cmd-whitelistadd-desc = Добавляет игрока с указанным именем в белый список.
cmd-whitelistadd-help = Использование: whitelistadd <username>
cmd-whitelistadd-existing = { $username } уже в белом списке!
cmd-whitelistadd-added = { $username } добавлен в белый список.
cmd-whitelistadd-not-found = Пользователь '{ $username }' не найден.
cmd-whitelistadd-arg-player = [player]
cmd-whitelistremove-desc = Удаляет игрока с указанным именем из белого списка сервера.
cmd-whitelistremove-help = Использование: whitelistremove <username>
cmd-whitelistremove-existing = { $username } не в белом списке!
cmd-whitelistremove-removed = Пользователь { $username } удалён из белого списка.
cmd-whitelistremove-not-found = Пользователь '{ $username }' не найден.
cmd-whitelistremove-arg-player = [player]
cmd-kicknonwhitelisted-desc = Кикает с сервера всех пользователей не из белого списка.
cmd-kicknonwhitelisted-help = Использование: kicknonwhitelisted
ban-banned-permanent = Этот бан можно только обжаловать.
ban-banned-permanent-appeal = Этот бан можно только обжаловать. Вы можете подать обжалование на { $link }
ban-expires = Вы получили бан на { $duration } минут, и он истечёт { $time } по UTC (для московского времени добавьте 3 часа).
ban-banned-1 = Вам, или другому пользователю этого компьютера или соединения, запрещено здесь играть.
ban-banned-2 = Вас заблокировал: "{ $adminName }"
ban-banned-3 = Причина бана: "{ $reason }"
ban-banned-4 = Попытки обойти этот бан через создание новых аккаунтов будут залогированы.
soft-player-cap-full = Сервер заполнен!
panic-bunker-account-denied = Этот сервер находится в режиме бункера паники, который часто включается в качестве меры предосторожности против набегаторов. Новые подключения от учётных записей, не соответствующих определённым требованиям, временно не принимаются. Повторите попытку позже
whitelist-playtime = You do not have enough playtime to join this server. You need at least { $minutes } minutes of playtime to join this server.
whitelist-player-count = This server is currently not accepting players. Please try again later.
whitelist-notes = You currently have too many admin notes to join this server. You can check your notes by typing /adminremarks in chat.
whitelist-manual = You are not whitelisted on this server.
whitelist-blacklisted = You are blacklisted from this server.
whitelist-always-deny = You are not allowed to join this server.
whitelist-fail-prefix = Not whitelisted: { $msg }
whitelist-misconfigured = The server is misconfigured and is not accepting players. Please contact the server owner and try again later.
cmd-blacklistadd-desc = Adds the player with the given username to the server blacklist.
cmd-blacklistadd-help = Usage: blacklistadd <username>
cmd-blacklistadd-existing = { $username } is already on the blacklist!
cmd-blacklistadd-added = { $username } added to the blacklist
cmd-blacklistadd-not-found = Unable to find '{ $username }'
cmd-blacklistadd-arg-player = [player]
cmd-blacklistremove-desc = Removes the player with the given username from the server blacklist.
cmd-blacklistremove-help = Usage: blacklistremove <username>
cmd-blacklistremove-existing = { $username } is not on the blacklist!
cmd-blacklistremove-removed = { $username } removed from the blacklist
cmd-blacklistremove-not-found = Unable to find '{ $username }'
cmd-blacklistremove-arg-player = [player]
panic-bunker-account-denied-reason = Этот сервер находится в режиме бункера паники, который часто включается в качестве меры предосторожности против набегаторов. Новые подключения учётных записей, не соответствующих определённым требованиям, временно не принимаются. Повторите попытку позже. Причина: "{ $reason }"
panic-bunker-account-reason-account = Ваша учётная запись Space Station 14 слишком новая. Она должна быть старше { $minutes } минут
panic-bunker-account-reason-overall =
    Ваше общее игровое время на сервере должно быть больше { $minutes } { $minutes ->
        [one] минуты
       *[other] минут
    }.
baby-jail-account-denied = Этот сервер - сервер для новичков, предназначенный для новых игроков и тех, кто хочет им помочь. Новые подключения слишком старых или не внесенных в белый список аккаунтов не принимаются. Загляните на другие серверы и посмотрите все, что может предложить Space Station 14. Веселитесь!
baby-jail-account-denied-reason = Этот сервер - сервер для новичков, предназначенный для новых игроков и тех, кто хочет им помочь. Новые подключения слишком старых или не внесенных в белый список аккаунтов не принимаются. Загляните на другие серверы и посмотрите все, что может предложить Space Station 14. Веселитесь! Причина: "{ $reason }"
baby-jail-account-reason-account = Ваша учетная запись Space Station 14 слишком старая. Она должен быть моложе { $minutes } минут
baby-jail-account-reason-overall =
    Ваше общее игровое время на сервере должно быть меньше { $minutes } { $minutes ->
        [one] минуты
       *[other] минут
    }.
