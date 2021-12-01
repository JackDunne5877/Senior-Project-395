package edu.cwru.csds395.datinggame;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import javax.naming.InitialContext;
import javax.sql.DataSource;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;

@Path("user/{userId}")
public class User
{
	public static final String JNDI_DATING_GAME = "java:/comp/env/jdbc/datinggame";
	DataSource dataSource;
	Connection connection;
	private PreparedStatement selectWithUserId;

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
}
