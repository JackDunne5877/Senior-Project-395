package edu.cwru.csds395.datinggame;

import java.io.StringReader;
import java.sql.Connection;
import java.sql.Date;
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

@Path("/newuser")
public class NewUser
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

		System.out.println(object.getString("username"));
		System.out.println(object.getString("password"));
		System.out.println(object.getString("first_name"));
		System.out.println(object.getString("last_name"));
		System.out.println(object.getString("birthday"));

		if (checkForUser(object.getString("username")))
		{
			return Response.status(400).entity("User taken").build();
		}

		// insert user
		insert(object.getString("username"), object.getString("password"), object.getString("first_name"), object.getString("last_name"), object.getString("birthday"));

		return Response.status(200).entity("success").build();
	}

	// insert into sql database
	public void insert(String username, String password, String first_name, String last_name, String birthday) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates insert statement
		insertUser = connection.prepareStatement(
					"insert into user (username, password, first_name, last_name, birthday) values (?, ?, ?, ?, ?);"
				);

		// fill in parameters of insert statement
		int parameterIndex = 1;
		insertUser.setString(parameterIndex++, username);
		insertUser.setString(parameterIndex++, password);
		insertUser.setString(parameterIndex++, first_name);
		insertUser.setString(parameterIndex++, last_name);
		insertUser.setDate(parameterIndex++, Date.valueOf(birthday));

		// execute insert statement
		insertUser.executeUpdate();
	}

	// insert into sql database
	public boolean checkForUser(String username) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUsername = connection.prepareStatement(
					"select count(username) from user where username = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUsername.setString(parameterIndex++, username);

		// execute insert statement
		ResultSet result = selectWithUsername.executeQuery();

		// check if result exists
		if (result.next())
		{
			// count of users with the input username
			int count = result.getInt(1);
			// return true if user exists
			if (count > 0)
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
			throw new Exception();
		}
	}
}
