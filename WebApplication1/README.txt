# WebApplication1 API

This is a backend API built with **ASP.NET 8.0** that manages customer and order data.  
It provides REST endpoints for CRUD operations and is deployed via Docker on **Render**.

---

## API Endpoints

- `GET /api/customer/customer-detail/{id}` - Get customer by ID  
- `POST /api/customers/Submit` - Create a new customer  
- `DELETE /api/customers/Delete/{id}` - Delete a customer (soft delete using `isActive` flag)  

**Render URL:** [https://isd-test-1.onrender.com]


---

## Database

- The "database" for this project is a **JSON file** that is loaded into memory when the application starts.  
- File location: `Data/CustomerDatabase.json`
- All CRUD operations work on this in-memory collection.  
- Changes are **not persisted** back to the JSON file automatically; restarting the app reloads the original JSON data.

---

## Validations

- **Get customer by ID:**  
  - Returns **Bad Request** if the customer does not exist in the database.

- **Create customer:**  
  - Ensures `firstName` and `lastName` are not null before creating a record.  
  - Can be improved by adding a unique identifier (e.g., NRIC) and checking for duplicates before creation.

- **Delete customer:**  
  - Soft delete is implemented using the `isActive` flag.  
  - Returns **Bad Request** if the customer does not exist before deletion.

> Improvements: Authentication can be added to secure endpoints.

---

## Logging

This project uses **Serilog** for logging. Logs are written to files with **daily rolling intervals**, so each day has its own log file.

