# Ecommerce API

This is an Ecommerce API built with .NET 8.0. It provides various endpoints for managing products, categories, orders, users, and more.

For frontend [Click Here](https://github.com/Kuuw/ecommerceFE)

For database [Click Here](https://github.com/Kuuw/eCommerceDB)

## Technologies Used

- .NET 8.0
- Entity Framework Core
- AutoMapper
- Azure Storage Blobs
- FluentValidation
- JWT Authentication
- ImageSharp
- Swashbuckle (Swagger)

## API Endpoints

### Product Endpoints

- **GET /Product/{id}** - Get product by ID
- **POST /Product/GetPaged** - Get paged products
- **POST /Product** - Add a new product
- **PUT /Product/{id}** - Update a product
- **DELETE /Product/{id}** - Delete a product
- **PUT /Product/Stock/{id}** - Update product stock
- **POST /Product/Image/{id}** - Upload product image
- **DELETE /Product/Image/{id}** - Delete product image

### Category Endpoints

- **GET /Category** - Get all categories
- **GET /Category/{id}** - Get category by ID
- **POST /Category** - Add a new category
- **PUT /Category/{id}** - Update a category
- **DELETE /Category/{id}** - Delete a category

### User Endpoints

- **POST /User/Register** - Register a new user
- **POST /User/Login** - Login a user
- **GET /User** - Get current user details
- **PUT /User** - Update current user details

### Order Endpoints

- **GET /Order** - Get all orders for the current user
- **GET /Order/{id}** - Get order by ID
- **POST /Order** - Create a new order
- **DELETE /Order/{id}** - Delete an order

### Cart Endpoints

- **GET /Cart** - Get cart items for the current user
- **PUT /Cart** - Update cart item
- **DELETE /Cart/{productId}** - Delete cart item

### Address Endpoints

- **GET /Address** - Get addresses for the current user
- **GET /Address/{id}** - Get address by ID
- **POST /Address** - Add a new address
- **PUT /Address/{id}** - Update an address
- **DELETE /Address/{id}** - Delete an address

### Shipment Company Endpoints

- **GET /ShipmentCompany** - Get all shipment companies
- **GET /ShipmentCompany/{id}** - Get shipment company by ID
- **POST /ShipmentCompany** - Add a new shipment company
- **PUT /ShipmentCompany/{id}** - Update a shipment company
- **DELETE /ShipmentCompany/{id}** - Delete a shipment company

## Running the Project

To run the project, use the following command:

```sh
./runserver.sh
```

## License

This project is licensed under the MIT License.