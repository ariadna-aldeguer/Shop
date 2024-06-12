# Product API
### The Product API provides the following endpoints:
- **GET /Product:** Retrieves a list of products. You can filter the results by description, size, color, and price, and paginate the results using the PageNumber and PageSize parameters.
- **POST /Product:** Creates a new product. You need to provide a ProductCommand object in the request body.
- **PUT /Product/{id}:** Updates an existing product. You need to provide the product ID in the path and a ProductCommand object in the request body.
- **DELETE /Product/{id}:** Deletes an existing product. You need to provide the product ID in the path.
### The ProductCommand object has the following properties:
- **description:** The description of the product.
- **price:** The price of the product.
- **size:** The size of the product.
- **color:** The color of the product.
### The ProductDto object, which is returned by the GET /Product and POST /Product endpoints, has the following properties:
- **id**: The ID of the product.
- **description**: The description of the product.
- **size**: The size of the product.
- **color**: The color of the product.
- **price**: The price of the product.
### The GET /Product endpoint returns a ProductDtoPaginatedList object, which includes a list of ProductDto objects and pagination information.