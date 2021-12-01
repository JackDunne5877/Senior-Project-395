package edu.cwru.csds395.datinggame;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import javax.naming.InitialContext;
import javax.sql.DataSource;
import javax.ws.rs.GET;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Response;

@Path("user/{userId}")
public class User
{
	public static final String JNDI_DATING_GAME = "java:/comp/env/jdbc/datinggame";
	DataSource dataSource;
	Connection connection;
	private PreparedStatement selectWithUserId;
	private PreparedStatement insertProfilePicture;

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
}
