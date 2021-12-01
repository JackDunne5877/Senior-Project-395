package edu.cwru.csds395.datinggame;

import java.io.StringReader;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import javax.json.Json;
import javax.json.JsonObject;
import javax.json.JsonReader;
import javax.naming.InitialContext;
import javax.sql.DataSource;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

@Path("/login")
public class Login
{
	public static final String JNDI_DATING_GAME = "java:/comp/env/jdbc/datinggame";
	DataSource dataSource;
	Connection connection;
	protected PreparedStatement insertUser;
	protected PreparedStatement selectWithUsername;

	@POST
	// @Consumes(MediaType.TEXT_HTML)
	@Produces(MediaType.TEXT_HTML)
	public Response newTodo(String input) throws Exception
	{
		System.out.println(input);
		// parses Json input
		JsonReader jsonReader = Json.createReader(new StringReader(input));
		JsonObject object = jsonReader.readObject();
		jsonReader.close();

		// check if user with given username exists in database
		if (checkForUserWithPass(object.getString("username"),object.getString("password")))
		{
			return Response.status(200).entity("success").build();
		}
		else
		{
			return Response.status(400).entity("Invalid credentials").build();
		}
	}

	// confirm with sql database
	public boolean checkForUserWithPass(String username, String inputPassword) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUsername = connection.prepareStatement(
					"select password from user where username = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUsername.setString(parameterIndex++, username);

		// execute insert statement
		ResultSet result = selectWithUsername.executeQuery();

		// check if result exists
		if (result.next())
		{
			// store password of user with the input username
			String realPassword = result.getString(1);
			// return true if input password is equal to the stored password
			if (realPassword.equals(inputPassword))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
}
