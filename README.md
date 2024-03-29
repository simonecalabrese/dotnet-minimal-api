A simple web api with a few essential features for user authentication made with **.NET 7.0** and **MongoDB**.

The users passwords are hashed and stored in the database using the [Bcrypt](https://it.wikipedia.org/wiki/Bcrypt) algorithm.

## Set up

You'll need to add a certificate for the JWT authentication using the [JWT.Net](https://github.com/jwt-dotnet/jwt) package, so don't forget to change the default example certificate with your personal certificate (and its key) inside the `certificates` folder if you're gonna use it for your personal projects.

The certificate is required in order to generate different tokens to issue to the users.

## Routes

There are 3 main routes:

- POST `/api/authentication/signup`
- POST `/api/authentication/login`
- GET `/api/authentication/profile`

### POST `/api/authentication/signup`

```json
{
  "name": "registered_name",
  "email": "registered_email@test.com",
  "password": "registered_password",
  "role": "user"
}
```

It stores a new user inside the database.

### POST `/api/authentication/login`

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

### GET `/api/authentication/profile`

```json
"Authorization": "Bearer <YOUR_TOKEN>"
```

It requires a JWT token set in your `Authorization` header and send you back your user registered information.
