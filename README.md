# Ecommerce.Store API

## Project Overview

**Ecommerce.Store** is an online store API built with **ASP.NET Core Web API**. The API supports the management of products, orders, and user authentication. It also integrates with **Stripe** for payment processing and uses **PostgreSQL** as the database. Swagger is included to provide easy access to the API documentation.

---

## Features

- **Admin Features**:
  - Manage products (create, read, update, delete).
  - Manage orders.
- **User Features**:
  - User registration and login (with JWT authentication).
  - Place orders.
- **Stripe Integration**:
  - Payment gateway for processing payments.
- **API Documentation**:
  - Swagger UI for easy interaction with the API.

---

## Technologies Used

- **Backend**: ASP.NET Core Web API
- **Frontend**: (Not included in this repo, can be developed with Angular/React)
- **Database**: PostgreSQL
- **Authentication**: JWT (JSON Web Token)
- **Payment Gateway**: Stripe
- **API Documentation**: Swagger UI

---

## Prerequisites

To run the project locally, you need the following:

- **.NET SDK 7.0 or higher**: [Install .NET SDK](https://dotnet.microsoft.com/download)
- **PostgreSQL**: [Install PostgreSQL](https://www.postgresql.org/download/)
- **Stripe API Keys**: [Create a Stripe account](https://stripe.com)
- **Visual Studio** or **Visual Studio Code** for development.

---

## Getting Started

### 1. Clone the Repository

Clone this repository to your local machine using Git:

```bash
git clone https://github.com/AkramKhattab/Ecommerce.Store.git
cd Ecommerce.Store
```

2. Set Up the Database
You need to create two PostgreSQL databases:

Ecommerce.Store.App (for storing product and order data).
Ecommerce.Store.Identity (for storing user data).
You can set up PostgreSQL locally or use a cloud-hosted solution like Azure PostgreSQL.

3. Update the Connection Strings
In your appsettings.json, update the connection strings to point to your PostgreSQL databases:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=Ecommerce.Store.App;Username=your-username;Password=your-password",
    "IdentityConnection": "Host=localhost;Database=Ecommerce.Store.Identity;Username=your-username;Password=your-password"
  }
}
```

4. Apply Migrations
Run the following commands to apply the database migrations and set up the tables:

```bash
dotnet ef database update --context EcommerceDbContext
dotnet ef database update --context StoreIdentityDbContext
```

5. Run the Project
Once the setup is complete, run the project using:

```bash
dotnet run
```

The API will start and will be accessible at:

http://localhost:5000 (non-secure)

https://localhost:5001 (secure)

Swagger UI will be available at:
```bash
http://localhost:5000/swagger
```

---

## API Endpoints
Here are some of the key API endpoints:

POST /api/auth/register: Register a new user.

POST /api/auth/login: Log in and get a JWT token.

GET /api/products: Get a list of all products.

GET /api/products/{id}: Get details of a single product.

POST /api/products: Add a new product (Admin only).

PUT /api/products/{id}: Update a product (Admin only).

DELETE /api/products/{id}: Delete a product (Admin only).

POST /api/orders: Create a new order.

GET /api/orders: Get all orders (Admin only).

---

## Authentication
This API uses JWT (JSON Web Tokens) for authentication. Here’s how it works:

Register: Send a POST request to /api/auth/register with user details (e.g., username, email, password).

Login: Send a POST request to /api/auth/login with your username and password to receive a JWT token.

Access Protected Endpoints: To access protected routes like creating products or viewing orders, you need to pass the JWT token in the request header:
```bash
Authorization: Bearer your-jwt-token
```


---
## Stripe Integration
Stripe is used for payment processing. To use it:

Sign up for Stripe: Create a Stripe account.
Get your API keys: From the Stripe dashboard, obtain your Publishable Key and Secret Key.
Configure Stripe: Update your appsettings.json with your Stripe keys:

```json
{
  "Stripe": {
    "Publishablekey": "your-publishable-key",
    "SecretKey": "your-secret-key"
  }
}
```

---

## Testing
To run the unit tests for the project, navigate to the Ecommerce.Store.Tests folder and run:
```bash
dotnet test
```

---

## Deployment
Deploying to Azure
Create an Azure App Service:

Go to the Azure Portal and create a new App Service.
Set the runtime stack to ASP.NET Core.
Create and configure Azure PostgreSQL databases.
Publish the Application:

You can deploy directly from Visual Studio using the Publish button, or you can use Azure CLI or GitHub Actions to automate the deployment process.
Other Hosting Platforms
This project can be hosted on other platforms such as Heroku, Render, or Railway for free or low-cost hosting solutions.

---

## License
This project is licensed under the MIT License. See the LICENSE file for details.

---

## Contribution
Contributions are welcome! If you would like to improve the project, fork the repository and submit a pull request. Please open an issue for bug reports or feature requests.
