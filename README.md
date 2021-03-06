# DoAnQuanLyNhaTro_WebASP-NET
 #Create By Lê Hoàng Long.
Phần mềm desktop kết hợp với ASP.NET quản lý user

Tiếng Việt
1. Điều kiện chạy phần mềm
  - Cài đặt hệ quản trị cơ sở dữ liệu SQL server Express.
  - Cài đặt Devexpress.
  - Nên chạy trên Visual Studio 2012 để tránh các lỗi không mong muốn.
2. Cách thức chạy project
  - Vì phần mềm nằm chung một solution nên cần đặt chế độ debug ở Multiple startup project và debug từng phần.
3. Thành phần Solution
  - BLL_DAL: Là một dll kết nối giữa tầng giao diện và tầng dữ liệu giúp trao đổi dữ liệu(CRUD). Sử dụng library LINQ TO SQL để kết nối 
  - GUI: Là phần giao diện desktop. Có sử dụng Devexpress để xây dựng giao diện đẹp mắt và nhanh gọn.
  - WEB: Là phần giao diện web người quản trị. Sử dụng framework ASP.NET MVC để xây dựng, có sử dụng css, javascripts, jquery, và một
framework css là Bootstrap để xây dựng giao diện.
4. Nghiệp vụ phần mềm
  - Phía user có thể sử dụng giao diện desktop để thực hiện nghiệp vụ quản lý phòng trọ
    - Quản lý thuê - trả
    - Quản lý khách hàng
    - Quản lý Quản lý phòng trọ
  - Phía quản lý user có thể sử dụng giao diện Web để thực hiện nghiệp vụ quản lý user
    - CRUD user.
    - Phân quyền user.
    - Thống kê : Khách hàng, phòng trống...
    - Báo cáo doanh thu: Từng tháng trong năm, Từng năm.
  - Một số tài khoản để sử dụng.
    - Web : TK:admin, MK:admin.
    - Desktop : TK:lehoanglong, Mk:123456789 (full quyền). TK:nguyena, Mk:123AAA (quyền truy cập thuê - trả).
5. Một số công nghệ áp dụng
   - Sử dụng thuật toán KNN để tư vấn cho người chủ trọ giá phòng trọ mặt bằng chung của khu vực và diện tích đó.
     - Thuật toán dựa trên dữ liệu mẩu khoản 2000 record, sử dụng kỹ thuật Crawl dữ liệu từ trang web batdongsan.com.vn.
   - Sử dụng MailMessgae và SmtpClient để gửi hóa đơn hàng tháng qua mail của khách hàng.
   - Mã hóa mật khẩu theo md5.

English
1. Software running conditions
  - Install SQL server Express database management system.
  - Install Devexpress.
  - Should run on Visual Studio 2012 to avoid unexpected errors.
2. How to run the project
  - Because the software is in the same solution, it is necessary to set the debug mode in Multiple startup project and debug each part.
3. Solution Ingredients
  - BLL_DAL: Is a dll that connects the interface layer and the data layer to help exchange data (CRUD). Use LINQ TO SQL library to connect
  - GUI: Is the desktop interface part. Using Devexpress to build a beautiful and fast interface.
  - WEB: The web interface part of the administrator. Using the ASP.NET MVC framework to build, using css, javascripts, jquery, and a
framework css is Bootstrap to build the interface.
4. Software business
  - The user side can use the desktop interface to perform room management
    - Rent - pay management
    - Customer management
    - Managing Room Management
  - The user management side can use the Web interface to perform user management operations
    - CRUD users.
    - User permissions.
    - Statistics: Customers, room availability...
    - Revenue report: Every month of the year, Every year.
  - Several accounts to use.
    - Web: TK:admin, MK:admin.
    - Desktop: TK:lehoanglong, Mk:123456789 (full permissions). TK:nguyena, Mk:123AAA (rent-pay access).
5. Some applied technologies
   - Use the KNN algorithm to advise the innkeeper on the price of the room and the common ground of that area and area.
     - The algorithm is based on sample data of about 2000 records, using data Crawl technique from website batdongsan.com.vn.
   - Use MailMessgae and SmtpClient to send monthly invoices via email of customers.
   - Encrypt password according to md5.
