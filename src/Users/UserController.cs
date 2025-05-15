using System.Collections;
using System.Net;


     namespace SimpleMDB;

    public class UserController
    {
        private UserServices userServices;

        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        public async Task ViewAllGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
        {
            int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
            int size = int.TryParse(req.QueryString["size"], out int s) ? s : 5;

            // Asegurar que page y size sean positivos
            if (page < 1) page = 1;
            if (size < 1) size = 5;

            Result<PageResult<User>> result = await userServices.ReadAll(page, size);

            if (result.IsValid)
            {
                PageResult<User> pagedResult = result.Value!;
                List<User> users = pagedResult.Values;
                int userCount = pagedResult.TotalCount;
                int pageCount = (int)Math.Ceiling((double)userCount / size);

                string rows = "";

                foreach (var user in users)
                {
                    rows += @$"
                    <tr>
                        <td>{user.Id}</td>
                        <td>{user.Username}</td>
                        <td>{user.Password}</td>
                        <td>{user.Salt}</td>
                        <td>{user.Role}</td>
                    </tr>";
                }

                // Limitar valores de paginación para no ir fuera de rango
                int prevPage = page > 1 ? page - 1 : 1;
                int nextPage = page < pageCount ? page + 1 : pageCount;

                string html = $@"
                <table border=""1"">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Username</th>
                            <th>Password</th>
                            <th>Salt</th>
                            <th>Role</th>
                        </tr>
                    </thead>
                    <tbody>
                        {rows}
                    </tbody>
                </table>
                <div>
                    <a href=""?page=1&size={size}"">First</a>
                    <a href=""?page={prevPage}&size={size}"">Prev</a>
                    <span>{page} / {pageCount}</span>
                    <a href=""?page={nextPage}&size={size}"">Next</a>
                    <a href=""?page={pageCount}&size={size}"">Last</a>
                </div>";

                string content = HtmlTemplate.Base("SimpleMDB", "Users View All Page", html);

        
            }
            else
            {
                // Opcional: responder con error si falla la consulta
                await HttpUtils.Respond(req, res, options, (int)HttpStatusCode.InternalServerError, 
                    "<h1>Failed to load users</h1>");
            }
        }
    }
