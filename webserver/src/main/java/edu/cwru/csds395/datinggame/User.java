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
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

@Path("user/{userId}")
public class User
{
	public static final String JNDI_DATING_GAME = "java:/comp/env/jdbc/datinggame";
	DataSource dataSource;
	Connection connection;
	private PreparedStatement getUser;
	private PreparedStatement selectWithUserId;
	private PreparedStatement insertProfilePicture;
	private PreparedStatement updateEmail;
	private PreparedStatement updatePassword;
	private PreparedStatement deleteUser;

	/** get first and last name from database */
	@GET
	@Produces("application/html")
	public String getName(@PathParam("userId") int userId) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select first_name, last_name from user where id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userId);

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// format and return first and last name
			return result.getString(1) + " " + result.getString(2);
		}
		else
		{
			return null;
		}
	}

	/** get birthday from database */
	@GET
	@Path("birthday")
	@Produces("application/html")
	public String getBirthday(@PathParam("userId") int userId) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select birthday from user where id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userId);

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// return date
			return result.getString(1);
		}
		else
		{
			return null;
		}
	}

	/** get bio from database */
	@GET
	@Path("bio")
	@Produces("application/html")
	public String getBio(@PathParam("userId") int userId) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select bio from user where id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userId);

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// return date
			return result.getString(1);
		}
		else
		{
			return null;
		}
	}

	/** get gender from database */
	@GET
	@Path("gender")
	@Produces("application/html")
	public String getGender(@PathParam("userId") int userId) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select gender from user where id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userId);

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// return date
			return result.getString(1);
		}
		else
		{
			return null;
		}
	}

	/** get user profile picture from database */
	@GET
	@Path("profilePicture")
	public byte[] getProfilePicture(@PathParam("userId") int userID) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select image from profile_picture where user_id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userID);
		//xt().lookup(JNDI_DATING_GAME));

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// return user profile picture
			return result.getBytes(1);
		}
		else
		{
			return null;
		}
	}

	/** put a profile picture into database */
	@PUT
	@Path("profilePicture")
	public Response addProfilePicture(byte[] input, @PathParam("userId") int userId) throws Exception
	{
		// log received image
		java.util.logging.Logger.getLogger(User.class.getCanonicalName())
			.log(
				java.util.logging.Level.INFO,
				"image data (hex): {0}",
				javax.xml.bind.DatatypeConverter.printHexBinary(input)
			);

		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();

		// try to update existing image
		insertProfilePicture = connection.prepareStatement(
				"update profile_picture set image = ? where user_id = ?;"
			);

		// fill in parameters of insert statement
		int parameterIndex = 1;
		insertProfilePicture.setBytes(parameterIndex++, input);
		insertProfilePicture.setInt(parameterIndex++, userId);

		// execute insert statement
		if (insertProfilePicture.executeUpdate() == 1)
		{
			return Response.status(200).entity("success").build();
		}

		// creates insert statement for new profile picture
		insertProfilePicture = connection.prepareStatement(
					"insert into profile_picture set (user_id, image) values (?, ?);"
				);

		// fill in parameters of insert statement
		parameterIndex = 1;
		insertProfilePicture.setInt(parameterIndex++, userId);
		insertProfilePicture.setBytes(parameterIndex++, input);

		// execute insert statement
		insertProfilePicture.executeUpdate();

		return Response.status(200).entity("success").build();
	}

	@POST
	@Path("newemail")
	@Produces(MediaType.TEXT_HTML)
	public Response setNewAccountEmail(@PathParam("userId") int userId, String input) throws Exception
	{
		System.out.println(input);
		// parses Json input
		JsonReader jsonReader = Json.createReader(new StringReader(input));
		JsonObject object = jsonReader.readObject();
		jsonReader.close();

		// check if user with given username exists in database
		if (updateEmail(userId,object.getString("password"), object.getString("newEmailAddress")))
		{
			return Response.status(200).entity("success").build();
		}
		else
		{
			return Response.status(400).entity("Invalid credentials").build();
		}
	}

	/** update email in sql database */
	public boolean updateEmail(int userId, String inputPassword, String newEmail) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select count(id) from user where id = ? and password = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userId);
		selectWithUserId.setString(parameterIndex++, inputPassword);

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// count of users with the input username
			int count = result.getInt(1);
			// return true if user exists
			if (count > 0)
			{
				// creates update statement
				updateEmail = connection.prepareStatement(
							"update user set email = ? where id = ? and password = ?;"
						);

				// fill in parameters of select statement
				parameterIndex = 1;
				updateEmail.setString(parameterIndex++, newEmail);
				updateEmail.setInt(parameterIndex++, userId);
				updateEmail.setString(parameterIndex++, inputPassword);

				// execute insert statement
				int updated = updateEmail.executeUpdate();

				return updated == 1;
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

	@POST
	@Path("newpassword")
	@Produces(MediaType.TEXT_HTML)
	//@Consumes(MediaType.APPLICATION_JSON)
	public Response setNewAccountPassword(@PathParam("userId") int userId, String input) throws Exception
	{
		System.out.println(input);
		// parses Json input
		JsonReader jsonReader = Json.createReader(new StringReader(input));
		JsonObject object = jsonReader.readObject();
		jsonReader.close();

		// check if user with given username exists in database
		if (updatePassword(userId,object.getString("password"), object.getString("newPassword")))
		{
			return Response.status(200).entity("success").build();
		}
		else
		{
			return Response.status(400).entity("Invalid credentials").build();
		}
	}

	/** update password in sql database */
	public boolean updatePassword(int userId, String inputPassword, String newPassword) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		selectWithUserId = connection.prepareStatement(
					"select count(id) from user where id = ? and password = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		selectWithUserId.setInt(parameterIndex++, userId);
		selectWithUserId.setString(parameterIndex++, inputPassword);

		// execute insert statement
		ResultSet result = selectWithUserId.executeQuery();

		// check if result exists
		if (result.next())
		{
			// count of users with the input username
			int count = result.getInt(1);
			// return true if user exists
			if (count > 0)
			{
				// creates update statement
				updatePassword = connection.prepareStatement(
							"update user set password = ? where id = ? and password = ?;"
						);

				// fill in parameters of select statement
				parameterIndex = 1;
				updatePassword.setString(parameterIndex++, newPassword);
				updatePassword.setInt(parameterIndex++, userId);
				updatePassword.setString(parameterIndex++, inputPassword);

				// execute insert statement
				int updated = updatePassword.executeUpdate();

				return updated == 1;
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

	/** confirm user with pass */
	@POST
	@Path("confirmPlayer")
	@Produces(MediaType.TEXT_HTML)
	public Response ConfirmPlayerPassword(@PathParam("userId") String userId, String input) throws Exception
	{
		System.out.println(input);
		// parses Json input
		JsonReader jsonReader = Json.createReader(new StringReader(input));
		JsonObject object = jsonReader.readObject();
		jsonReader.close();

		String password = object.getString("password");

		// check if user with given username exists in database
		if (confirmIdWithPass(userId, password))
		{
			return Response.status(200).entity("success").build();
		}
		else
		{
			return Response.status(400).entity("Invalid credentials").build();
		}
	}

	/** confirm with sql database */
	public boolean confirmIdWithPass(@PathParam("userId") String userId, String inputPassword) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		getUser = connection.prepareStatement(
					"select password from user where id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		getUser.setString(parameterIndex++, userId);

		// execute insert statement
		ResultSet result = getUser.executeQuery();

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

	/** delete user from database */
	@POST
	@Path("disableaccount")
	@Produces(MediaType.TEXT_HTML)
	public Response disableAccount(@PathParam("userId") String userId, String input) throws Exception
	{
		System.out.println(input);
		// parses Json input
		JsonReader jsonReader = Json.createReader(new StringReader(input));
		JsonObject object = jsonReader.readObject();
		jsonReader.close();

		// check if user with given username exists in database
		if (disableAccountWithPass(userId,object.getString("password")))
		{
			// TODO
			return Response.status(200).entity("success").build();
		}
		else
		{
			return Response.status(400).entity("Invalid credentials").build();
		}
	}

	/** confirm with sql database */
	public boolean disableAccountWithPass(@PathParam("userId") String userId, String inputPassword) throws Exception
	{
		// connect to database
		dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		connection = dataSource.getConnection();
		// creates select statement
		getUser = connection.prepareStatement(
					"select password from user where id = ?;"
				);

		// fill in parameters of select statement
		int parameterIndex = 1;
		getUser.setString(parameterIndex++, userId);

		// execute insert statement
		ResultSet result = getUser.executeQuery();

		// check if result exists
		if (result.next())
		{
			// store password of user with the input username
			String realPassword = result.getString(1);
			// deleted user and return true if input password is equal to the stored password
			if (realPassword.equals(inputPassword))
			{
				deleteUser = connection.prepareStatement(
						"delete from user where id = ?;"
						);

				// fill in parameters of select statement
				parameterIndex = 1;
				getUser.setString(parameterIndex++, userId);

				// execute insert statement
				result = getUser.executeQuery();
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
