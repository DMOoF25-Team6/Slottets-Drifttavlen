# Claims

Claims are a fundamental part of the authorization system, providing a way to define and manage permissions for users based on their roles and specific attributes. In this system, claims are categorized into two main types: role claims and user claims.

| Type        | ClaimType    | ClaimValue                |
|-------------|--------------|---------------------------|
| Role Claim  | permission   | manage:residents          |
| Role Claim  | permission   | view:medicine             |
| User Claim  | permission   | manage:residents          |
| User Claim  | permission   | department:slottet:basic  |
| User Claim  | permission   | department:skoven:basic   |
| User Claim  | permission   | department:skoven:basic   |
| User Claim  | permission   | department:marken:basic   |
| User Claim  | permission   | department:slottet:basic  |
| User Claim  | permission   | department:all:view       |

## role claims

Role claims are permissions assigned to specific roles within the system. Users who are assigned these roles will inherit the associated claims, allowing them to perform actions that are permitted by those claims.

| Target        | ClaimType    | ClaimValue                |
|---------------|--------------|---------------------------|
| admin         | permission   | manage:residents          |
| caretaker     | permission   | view:medicine             |

## User claims

User claims are specific permissions assigned to individual users, allowing for granular access control based on their roles and responsibilities. These claims can be used to determine what actions a user is authorized to perform within the system.
