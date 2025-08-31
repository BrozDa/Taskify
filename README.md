# 📝 Taskify – ToDo App with .NET & React

_A hands-on learning project built with ASP.NET Core Web API and React_

This project was created to deepen understanding of building a full-stack CRUD application with authentication, relational data handling, and responsive UI design.

Users can **create, manage, complete, or delete tasks** securely through **JWT-based authentication**.

---

## Features

- **Authentication & Registration**
  - Secure JWT token-based login
  - User registration and login flow
  - Redirect to dashboard after login

- **Initial Data Seeding**
  - Preloads priorities, sample tags, and admin users on first launch

- **Task Management**
  - Create new tasks with:
    - Priority (4 predefined levels)
    - Due date (future-only selection)
    - Name & Description
    - 0 or more Tags (custom, user-specific)
  - Edit any task field by double-clicking
  - Mark task as completed
  - Delete task entirely

- **Completed Tasks View**
  - View previously completed tasks (read-only)

- **Tag Management**
  - Create and assign personal tags to tasks
  - Tags are user-scoped (not shared)

- **Logout Support**
  - Secure logout to end session
  
 - **Mobile-Friendly Design**
    - Fully responsive layout using Tailwind

---

## Tech Stack

- **Backend**: ASP.NET Core Web API (.NET 8+)
- **Frontend**: React (JavaScript)
- **Styling**: Tailwind for modern, responsive UI (includes Dark Mode support)
- **Auth**: JWT Bearer Tokens
- **Database**: Entity Framework Core with SQL Server
- **State Management**: React Context API

## Architecture Notes

- Backend is built using controller-based ASP.NET Core Web API
- Follows RESTful conventions for resources like Tasks, Tags, and Auth
- Separation of concerns between controllers, models, and services

## Areas for Improvement

- Implement **refresh tokens** to improve authentication security
- Add **logging**
- Add **email verification** during registration for added  security
- Add **search and filtering** options for tasks and tags

## Running the app within a docker container
### Prerequisites:
- [Docker Desktop](https://docs.docker.com/desktop/) (includes Docker Engine, CLI, and Compose)
- Alternatively  which includes all 3 components

### Run the app:
```sh
docker compose up --build
```
  **Which**: 
  1. Starts a SQL Server 2022 instance
  2. Build and start the Taskify.API backend
  3. Build and start the Taskify.Web frontend
    
### Accessing components:
  - API → http://localhost:7024
  - Frontend → http://localhost:3000

## Screenshots
  ### Dashboard in light mode:
<img width="800" height="500" alt="image" src="https://github.com/user-attachments/assets/624d6921-e94d-4890-87ae-4ab4c18a8c56" />



  ### Completed tasks in dark mode:
<img width="800" height="500" alt="image" src="https://github.com/user-attachments/assets/c48291a1-946a-4957-aee4-f190bb2700d0" />



 ### Mobile version od dashboard in dark mode:
<img width="381" height="636" alt="image" src="https://github.com/user-attachments/assets/08c5c37e-5e3a-4688-ba1e-f467fd84aea9" />

