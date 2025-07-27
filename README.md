# 📝 Taskify – ToDo App with .NET & React

This is a full-stack CRUD application built with **ASP.NET Core (.NET)** and **React**, designed to help users manage tasks with support for priorities, tags, and authentication.

Users can **create, manage, complete, or delete tasks** securely through **JWT-based authentication**.

Built for learning and fun

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
- **Styling**: Tailwind for modern, responsive UI (includes Dark Mode support )
- **Auth**: JWT Bearer Tokens
- **Database**: Entity Framework Core (with SQLite or SQL Server)
- **State Management**: React Context API


## Areas for Improvement

- Implement **refresh tokens** to improve authentication security
- Add **logging**
- Add **email verification** during registration for added  security
- Add **search and filtering** options for tasks and tags
