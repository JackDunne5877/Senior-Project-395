package edu.cwru.csds395.datinggame;

import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

@Path("/")
public class ModifiedServlet
{
	@POST
	// @Consumes(MediaType.TEXT_HTML)
	@Produces(MediaType.TEXT_HTML)
	public void newTodo(String input)
	{
		System.out.println(input);
	}
}