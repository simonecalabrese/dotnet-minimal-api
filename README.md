A boiler-plate web api with user authentication made with **.NET 7.0** and **MongoDB**.

## Authentication

### Set up

There's a custom JWT authentication using the [JWT.Net](https://github.com/jwt-dotnet/jwt) package.
It needs a certificate to be initialized so don't forget to switch the default example certificate with your personal certificate (and its key) inside the certificates folder if you're gonna use it for your personal projects.

The certificate is required in order to generate different tokens to issue to the users.

### Routes

There are 3 main routes:

- POST `/api/authentication/signup`
- POST `/api/authentication/login`
- GET `/api/authentication/profile`

#### POST `/api/authentication/signup`

```json
{
  "name": "registered_name",
  "email": "registered_email@test.com",
  "password": "registered_password",
  "role": "user"
}
```

It stores a new user inside the database.

#### POST `/api/authentication/login`

```json
{
  "email": "registered_email@test.com",
  "password": "registered_password"
}
```

It verifies the credentials and send back a JWT token that you can set in your future requests `Authorization` header like this:

```json
"Authorization": "Bearer <YOUR_TOKEN>"
```

Now you can perform all of the requests as authenticated user

#### GET `/api/authentication/profile`

```json
"Authorization": "Bearer <YOUR_TOKEN>"
```

It requires a JWT token set in your `Authorization` header and send you back your user registered information.
