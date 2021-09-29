package edu.cwru.csds395.datinggame;

import java.io.StringReader;
import java.sql.Connection;
import java.sql.Date;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import javax.json.Json;
import javax.json.JsonObject;
import javax.json.JsonReader;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

@Path("/")
public class ModifiedServlet
{
	public static final String JNDI_DATING_GAME = "java:/comp/env/jdbc/datinggame";
	DataSource dataSource;
	Connection connection;
	protected PreparedStatement insertUser;

	@POST
	// @Consumes(MediaType.TEXT_HTML)
	@Produces(MediaType.TEXT_HTML)
	public void newTodo(String input) throws Exception
	{
		//JSONObject js = new JSONObject(input);

		Context initContext = new InitialContext();
		Context envContext  = (Context)initContext.lookup("java:/comp/env");
		DataSource ds = (DataSource)envContext.lookup("jdbc/datinggame");
		Connection conn = ds.getConnection();

		JsonReader jsonReader = Json.createReader(new StringReader(input));
		JsonObject object = jsonReader.readObject();
		jsonReader.close();

		System.out.println(object.getString("username"));
		System.out.println(object.getString("password"));
		System.out.println(object.getString("first_name"));
		System.out.println(object.getString("last_name"));
		System.out.println(object.getString("birthday"));

		insert(object.getString("username"), object.getString("password"), object.getString("first_name"), object.getString("last_name"), object.getString("birthday"));
	}

	// insert into sql database
	public void insert(String username, String password, String first_name, String last_name, String birthday)
	{
		try {
			dataSource = (DataSource)(new InitialContext().lookup(JNDI_DATING_GAME));
		}
		catch (NamingException e)
		{

		}
		try {
			connection = dataSource.getConnection();
			insertUser = connection.prepareStatement(
						"insert into user (username, password, first_name, last_name, birthday) values (?, ?, ?, ?, ?);"
					);
		}
		catch(SQLException e)
		{

		}
		try {

			int parameterIndex = 1;
			insertUser.setString(parameterIndex++, username);
			insertUser.setString(parameterIndex++, password);
			insertUser.setString(parameterIndex++, first_name);
			insertUser.setString(parameterIndex++, last_name);
			insertUser.setDate(parameterIndex++, Date.valueOf(birthday));
			insertUser.executeUpdate();
		}
		catch(SQLException e)
		{

		}
	}
}