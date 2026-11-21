# Notes 
[x] is NOT
[v] is YES


# Changelog



## [0.1.0] - [11/5/2026 - 19/11.2026] 
### Added
- `Email`, `PublicName`, `UserTag` value object with validation 
- `User`, `Account`, `AuthUser`, entities with private setters
- EF core(`Email isunique`) [Infrastructure/Persistence/Configurations/UserConfigurations.cs ]
    -- `DataBase` with `DbSet`(Users,Accounts)

## [0.1.1] -[20/5/2026 - now]
### Added 
- `UserDto`, `AuditsLogs`, `AuthUser` entities
- `OutboxService`, `Register + recordRegister =>[incomplete]`
- UserConfiguration => `IsUnique for tag/email` + connections 

## IN FUTURE: 
- `Polly` = [now] => [x]
- `Full Register` = [now] => [x]
- `OutboxJob` = [now] => [x]

