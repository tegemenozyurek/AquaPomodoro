## Database Tables and Columns

### 1. `users`
| Column        | Type               | Constraints                 |
|---------------|--------------------|-----------------------------|
| id            | SERIAL             | PRIMARY KEY                 |
| mail          | VARCHAR(255)       | NOT NULL, UNIQUE            |
| password      | VARCHAR(255)       | NOT NULL                   |
| coinBalance   | INT                | NOT NULL, DEFAULT 100       |
| totalFocusTime| INT                | NOT NULL, DEFAULT 0         |
| createdAt     | TIMESTAMP          | NOT NULL, DEFAULT CURRENT_TIMESTAMP |

---

### 2. `fishes`
| Column    | Type                | Constraints                             |
|-----------|---------------------|----------------------------------------|
| id        | SERIAL              | PRIMARY KEY                           |
| name      | VARCHAR(255)        | NOT NULL                             |
| price     | INT                 | NOT NULL                             |
| type      | ENUM                | NOT NULL, DEFAULT 'small' (small, medium, large, special) |
| gifURL    | VARCHAR(500)        | NOT NULL, DEFAULT 'no url'            |
| createdAt | TIMESTAMP           | NOT NULL, DEFAULT CURRENT_TIMESTAMP   |

---

### 3. `aquariums`
| Column   | Type      | Constraints                                 |
|----------|-----------|--------------------------------------------|
| id       | SERIAL    | PRIMARY KEY                               |
| userID   | INT       | NOT NULL, FOREIGN KEY → `users(id)`       |
| fishID   | INT       | NOT NULL, FOREIGN KEY → `fishes(id)`      |
| addedAt  | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP        |
