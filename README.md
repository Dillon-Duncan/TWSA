# TWSA - Two-Way South Africa

Welcome to TWSA (Two-Way South Africa), a C# .NET application designed to streamline municipal services in South Africa. The platform provides an efficient and user-friendly interface for citizens to access and request municipal services, report issues, and stay informed about local events and announcements.

## Table of Contents

1. [Overview](#overview)
2. [Key Features](#key-features)
3. [Technology Stack](#technology-stack)
4. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
5. [Usage](#usage)
6. [Data Structures](#data-structures)
7. [User Interface](#user-interface)
8. [Technical Implementation](#technical-implementation)
9. [Additional Features](#additional-features)
10. [License](#license)

## Overview

TWSA aims to bridge the gap between community members and local authorities by offering a comprehensive platform for engagement. The application empowers residents to actively participate in community improvement while providing municipal administrators with efficient tools to manage and respond to citizen needs.

## Key Features

- **Issue Reporting**: Easily report and track community issues
- **Service Requests**: Submit and monitor the status of service requests
- **Event Announcements**: Access information about local events and announcements
- **Two-Way Communication**: Direct messaging feature for citizen-authority interaction
- **User-Centric Design**: Intuitive interface optimized for both desktop and mobile devices
- **Administrative Tools**: Specialized features for municipal administrators

## Technology Stack

- C# ASP.NET Core MVC
- SQL Server (SSMS)
- Entity Framework Core

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or later
- SQL Server Management Studio (SSMS)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/Dillon-Duncan/TWSA.git
   cd TWSA
   ```

2. Restore dependencies:
   ```
   dotnet restore
   ```

3. Set up the database:
   - Open SSMS and connect to your local SQL Server instance
   - Create a new database named "TWSA"
   - Update the connection string in `appsettings.json` if necessary

4. Apply database migrations:
   ```
   dotnet ef database update
   ```

5. Run the application:
   ```
   dotnet run
   ```
   
## Usage

### For Citizens

1. **Register/Login**: Create an account or log in to access all features.
2. **Report an Issue**: Navigate to "Report Issues" and fill in the required details.
3. **View Local Events**: Browse upcoming events and announcements in your area.
4. **Track Service Requests**: Monitor the status of your submitted requests.
5. **Direct Messaging**: Communicate with municipal authorities through the messaging feature.

### For Administrators

1. **Manage Reports**: View and respond to reported issues.
2. **Post Events/Announcements**: Create and publish local events or important announcements.
3. **User Management**: Manage user accounts and grant administrative privileges.

## Data Structures

The application utilizes efficient data structures to manage information:

- **User**: Stores citizen and administrator information
- **Issue**: Manages reported community issues
- **Event/Announcement**: Handles local events and municipal announcements

## User Interface

TWSA features a responsive and user-friendly interface, including:

- Login and Registration pages
- Main Menu with navigation
- Issue Reporting form with dynamic progress feedback
- Event and Announcement browsing.
- Account Management settings

## Technical Implementation

- Utilizes stacks, queues, and priority queues for efficient data management
- Implements hash tables, dictionaries, and sorted dictionaries for optimized information retrieval
- Incorporates sets for handling unique categories and dates
- Features advanced algorithms for event recommendations based on user preferences

## Additional Features

- Consistent color scheme and layout for enhanced user experience
- Responsive design for seamless use across devices

## License

This project is licensed under the MIT License.
