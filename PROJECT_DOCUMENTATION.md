
## 🔄 LUỒNG XỬ LÝ CHÍNH

### 1. Luồng xem sản phẩm
```
User Request → ProductsController.Index() → ProductService.GetActiveProductsAsync()
    → Repository.GetAllAsync() → Database → Return Products → View Render
```

### 2. Luồng thêm vào giỏ hàng
```
User Click "Add to Cart" → CartController.AddToCart(productId, quantity)
    → Validate Product → CartService.GetCartItemByProductAsync()
    → If exists: Update Quantity | Else: Create New Cart Item
    → Update Session CartCount → Redirect
```

### 3. Luồng đặt hàng (Checkout)
```
User Submit Order → CheckoutController.ProcessOrder(model)
    → OrderService.ValidateCartInventoryAsync() → Validate Stock
    → OrderService.CreateOrderAsync() → Begin Transaction
    → Create Order → Create OrderItems → Update Product Stock
    → Commit Transaction → CartService.ClearCartAsync()
    → Redirect to OrderConfirmation
```

### 4. Luồng đăng nhập
```
User Submit Login → AccountController.Login(model)
    → UnitOfWork.Users.GetFirstOrDefaultAsync() → Find User
    → BCrypt.Verify() | Direct Compare → Validate Password
    → Set Session (UserId, Username, UserRole)
    → Redirect based on Role (Admin/Customer)
```

---

## ✅ CHỨC NĂNG ĐÃ HOÀN THÀNH

### 🛒 Module Sản phẩm (Product)
| STT | Chức năng | Backend | Frontend | Trạng thái |
|-----|-----------|---------|----------|------------|
| 1 | Hiển thị danh sách sản phẩm | ✅ | ✅ | Hoàn thành |
| 2 | Chi tiết sản phẩm | ✅ | ✅ | Hoàn thành |
| 3 | Tìm kiếm sản phẩm | ✅ | ✅ | Hoàn thành |
| 4 | Lọc theo danh mục | ✅ | ✅ | Hoàn thành |
| 5 | Lọc theo thương hiệu | ✅ | ✅ | Hoàn thành |
| 6 | Lọc theo khoảng giá | ✅ | ✅ | Hoàn thành |
| 7 | Sắp xếp sản phẩm | ✅ | ✅ | Hoàn thành |
| 8 | Phân trang | ✅ | ✅ | Hoàn thành |
| 9 | Hiển thị sản phẩm liên quan | ✅ | ✅ | Hoàn thành |
| 10 | Hiển thị giá sale | ✅ | ✅ | Hoàn thành |

### 🛍️ Module Giỏ hàng (Cart)
| STT | Chức năng | Backend | Frontend | Trạng thái |
|-----|-----------|---------|----------|------------|
| 1 | Thêm sản phẩm vào giỏ | ✅ | ✅ | Hoàn thành |
| 2 | Xem giỏ hàng | ✅ | ✅ | Hoàn thành |
| 3 | Cập nhật số lượng | ✅ | ✅ | Hoàn thành |
| 4 | Xóa sản phẩm khỏi giỏ | ✅ | ✅ | Hoàn thành |
| 5 | Xóa toàn bộ giỏ hàng | ✅ | ✅ | Hoàn thành |
| 6 | Hiển thị số lượng cart trên header | ✅ | ✅ | Hoàn thành |
| 7 | Giỏ hàng theo Session (Guest) | ✅ | ✅ | Hoàn thành |
| 8 | Giỏ hàng theo User | ✅ | ✅ | Hoàn thành |
| 9 | Migrate cart khi login | ✅ | ⚠️ | Cơ bản |

### 💳 Module Thanh toán (Checkout)
| STT | Chức năng | Backend | Frontend | Trạng thái |
|-----|-----------|---------|----------|------------|
| 1 | Form thông tin giao hàng | ✅ | ✅ | Hoàn thành |
| 2 | Validate thông tin đặt hàng | ✅ | ✅ | Hoàn thành |
| 3 | Kiểm tra tồn kho trước đặt hàng | ✅ | ✅ | Hoàn thành |
| 4 | Tạo đơn hàng | ✅ | ✅ | Hoàn thành |
| 5 | Tự động tạo mã đơn hàng | ✅ | ✅ | Hoàn thành |
| 6 | Trừ tồn kho sau đặt hàng | ✅ | ✅ | Hoàn thành |
| 7 | Trang xác nhận đơn hàng | ✅ | ✅ | Hoàn thành |
| 8 | Transaction cho đặt hàng | ✅ | N/A | Hoàn thành |

### 👤 Module Tài khoản (Account)
| STT | Chức năng | Backend | Frontend | Trạng thái |
|-----|-----------|---------|----------|------------|
| 1 | Đăng ký tài khoản | ✅ | ✅ | Hoàn thành |
| 2 | Đăng nhập | ✅ | ✅ | Hoàn thành |
| 3 | Đăng xuất | ✅ | ✅ | Hoàn thành |
| 4 | Mã hóa mật khẩu BCrypt | ✅ | N/A | Hoàn thành |
| 5 | Phân quyền Admin/Customer | ✅ | ✅ | Hoàn thành |
| 6 | Session management | ✅ | ✅ | Hoàn thành |
| 7 | Xem lịch sử đơn hàng | ✅ | ✅ | Hoàn thành |
| 8 | Xem chi tiết đơn hàng | ✅ | ✅ | Hoàn thành |

### ⚙️ Module Admin
| STT | Chức năng | Backend | Frontend | Trạng thái |
|-----|-----------|---------|----------|------------|
| 1 | Dashboard thống kê | ✅ | ✅ | Hoàn thành |
| 2 | Quản lý sản phẩm - Danh sách | ✅ | ✅ | Hoàn thành |
| 3 | Quản lý sản phẩm - Thêm mới | ✅ | ✅ | Hoàn thành |
| 4 | Quản lý sản phẩm - Chỉnh sửa | ✅ | ✅ | Hoàn thành |
| 5 | Quản lý sản phẩm - Xóa | ✅ | ✅ | Hoàn thành |
| 6 | Upload hình ảnh sản phẩm | ✅ | ✅ | Hoàn thành |
| 7 | Quản lý đơn hàng - Danh sách | ✅ | ✅ | Hoàn thành |
| 8 | Quản lý đơn hàng - Chi tiết | ✅ | ✅ | Hoàn thành |
| 9 | Cập nhật trạng thái đơn hàng | ✅ | ✅ | Hoàn thành |

### 🏠 Module Khác
| STT | Chức năng | Backend | Frontend | Trạng thái |
|-----|-----------|---------|----------|------------|
| 1 | Trang chủ | ✅ | ✅ | Hoàn thành |
| 2 | Trang liên hệ | ✅ | ✅ | Hoàn thành |
| 3 | Layout responsive | N/A | ✅ | Hoàn thành |
| 4 | Navigation header | N/A | ✅ | Hoàn thành |

---

## 🛠️ CÔNG NGHỆ SỬ DỤNG

| Loại | Công nghệ | Phiên bản |
|------|-----------|-----------|
| Framework | ASP.NET Core MVC | .NET 9.0 |
| ORM | Entity Framework Core | 9.x |
| Database | MySQL (Pomelo Provider) | 8.x |
| Frontend | Bootstrap, CSS Custom | 5.x |
| Password Hashing | BCrypt.Net | - |
| Session | ASP.NET Core Session | - |

---

## 📁 CHI TIẾT CÁC FILE CHÍNH

### GuhaStore.Core (Domain Layer)
```
Entities/
├── User.cs          # Thông tin người dùng
├── Product.cs       # Thông tin sản phẩm
├── Category.cs      # Danh mục sản phẩm
├── Brand.cs         # Thương hiệu
├── Cart.cs          # Giỏ hàng
├── Order.cs         # Đơn hàng
├── OrderItem.cs     # Chi tiết đơn hàng
└── ProductReview.cs # Đánh giá sản phẩm

Interfaces/
├── IRepository.cs      # Generic Repository Interface
├── IUnitOfWork.cs      # Unit of Work Pattern
├── IProductService.cs  # Product Business Interface
├── ICartService.cs     # Cart Business Interface
├── IOrderService.cs    # Order Business Interface
├── IEmailService.cs    # Email Interface
└── IFileUploadService.cs # File Upload Interface
```

### GuhaStore.Application (Application Layer)
```
Services/
├── ProductService.cs    # Xử lý logic sản phẩm
├── CartService.cs       # Xử lý logic giỏ hàng
├── OrderService.cs      # Xử lý logic đơn hàng
├── EmailService.cs      # Gửi email (placeholder)
└── FileUploadService.cs # Upload file
```

### GuhaStore.Infrastructure (Infrastructure Layer)
```
Data/
└── ApplicationDbContext.cs  # DbContext & Configurations

Repositories/
├── Repository.cs    # Generic Repository Implementation
└── UnitOfWork.cs    # Unit of Work Implementation

Migrations/
├── InitialSchema.cs       # Migration khởi tạo
└── AddIdentitySchema.cs   # Migration Identity
```

### GuhaStore.Web (Presentation Layer)
```
Controllers/
├── HomeController.cs      # Trang chủ
├── ProductsController.cs  # Quản lý hiển thị sản phẩm
├── CartController.cs      # Quản lý giỏ hàng
├── CheckoutController.cs  # Thanh toán
├── AccountController.cs   # Đăng nhập/Đăng ký
├── AdminController.cs     # Quản trị
└── ContactController.cs   # Liên hệ

Views/
├── Home/         # Views trang chủ
├── Products/     # Views sản phẩm
├── Cart/         # Views giỏ hàng
├── Checkout/     # Views thanh toán
├── Account/      # Views tài khoản
├── Admin/        # Views quản trị
│   ├── Products/ # CRUD sản phẩm
│   └── Orders/   # Quản lý đơn hàng
└── Shared/       # Layouts, Partials

Models/
├── LoginViewModel.cs
├── RegisterViewModel.cs
├── CheckoutViewModel.cs
└── ErrorViewModel.cs
```

---

# 👥 PHÂN CHIA CÔNG VIỆC CHO 5 THÀNH VIÊN

## Nguyên tắc phân chia
- Mỗi người đảm nhận **cả Frontend và Backend**
- Công việc được chia theo **Module/Feature**
- Mỗi phần có khối lượng tương đương nhau
- Bao gồm cả phần đã hoàn thành (review/maintain) và chưa hoàn thành (implement)

---

## 👤 THÀNH VIÊN 1: Module Sản phẩm & Tìm kiếm

### Backend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì ProductService | `GuhaStore.Application/Services/ProductService.cs` | ✅ Maintain |
| Duy trì IProductService | `GuhaStore.Core/Interfaces/IProductService.cs` | ✅ Maintain |
| Cải thiện Search (Full-text) | `ProductService.cs` | 🔄 Upgrade |
| Thêm Product Variants | `GuhaStore.Core/Entities/ProductVariant.cs` | 🆕 New |
| API Endpoint Products | `Controllers/Api/ProductsApiController.cs` | 🆕 New |

### Frontend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì trang Products Index | `Views/Products/Index.cshtml` | ✅ Maintain |
| Duy trì trang Product Details | `Views/Products/Details.cshtml` | ✅ Maintain |
| Cải thiện UI Filter | `Views/Products/Index.cshtml` | 🔄 Upgrade |
| Thêm Quick View Modal | `Views/Products/_QuickView.cshtml` | 🆕 New |
| Responsive improvements | CSS files | 🔄 Upgrade |


## 👤 THÀNH VIÊN 2: Module Giỏ hàng & Thanh toán

### Backend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì CartService | `GuhaStore.Application/Services/CartService.cs` | ✅ Maintain |
| Duy trì ICartService | `GuhaStore.Core/Interfaces/ICartService.cs` | ✅ Maintain |
| Duy trì CheckoutController | `GuhaStore.Web/Controllers/CheckoutController.cs` | ✅ Maintain |
| Tích hợp Coupon/Discount | `Services/CouponService.cs` | 🆕 New |
| Tính phí vận chuyển | `Services/ShippingService.cs` | 🆕 New |
| Validate stock realtime | `CartService.cs` | 🔄 Upgrade |

### Frontend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì Cart Page | `Views/Cart/Index.cshtml` | ✅ Maintain |
| Duy trì Checkout Page | `Views/Checkout/Index.cshtml` | ✅ Maintain |
| Duy trì Order Confirmation | `Views/Checkout/OrderConfirmation.cshtml` | ✅ Maintain |
| Mini Cart Dropdown | `Views/Shared/_MiniCart.cshtml` | 🆕 New |
| AJAX Add to Cart | `wwwroot/js/cart.js` | 🆕 New |
| Form validation UX | Checkout form | 🔄 Upgrade |


## 👤 THÀNH VIÊN 3: Module Tài khoản & Bảo mật

### Backend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì AccountController | `GuhaStore.Web/Controllers/AccountController.cs` | ✅ Maintain |
| Duy trì User Entity | `GuhaStore.Core/Entities/User.cs` | ✅ Maintain |
| Quên mật khẩu | `AccountController.cs` | 🆕 New |
| Đổi mật khẩu | `AccountController.cs` | 🆕 New |
| Cập nhật Profile | `AccountController.cs` | 🆕 New |
| Email verification | `Services/EmailService.cs` | 🆕 New |
| Session security | `Middleware/` | 🔄 Upgrade |

### Frontend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì Login Page | `Views/Account/Login.cshtml` | ✅ Maintain |
| Duy trì Register Page | `Views/Account/Register.cshtml` | ✅ Maintain |
| Duy trì Order History | `Views/Account/OrderHistory.cshtml` | ✅ Maintain |
| Forgot Password Page | `Views/Account/ForgotPassword.cshtml` | 🆕 New |
| Profile Edit Page | `Views/Account/EditProfile.cshtml` | 🆕 New |
| Change Password Page | `Views/Account/ChangePassword.cshtml` | 🆕 New |
| My Account Dashboard | `Views/Account/MyAccount.cshtml` | 🔄 Upgrade |


## 👤 THÀNH VIÊN 4: Module Admin & Quản lý đơn hàng

### Backend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì AdminController | `GuhaStore.Web/Controllers/AdminController.cs` | ✅ Maintain |
| Duy trì OrderService | `GuhaStore.Application/Services/OrderService.cs` | ✅ Maintain |
| Admin - Quản lý Users | `AdminController.cs` | 🆕 New |
| Admin - Quản lý Categories | `AdminController.cs` | 🆕 New |
| Admin - Quản lý Brands | `AdminController.cs` | 🆕 New |
| Export Orders to Excel | `Services/ExportService.cs` | 🆕 New |
| Dashboard Statistics | `Services/AnalyticsService.cs` | 🆕 New |

### Frontend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì Admin Dashboard | `Views/Admin/Index.cshtml` | ✅ Maintain |
| Duy trì Admin Products | `Views/Admin/Products/` | ✅ Maintain |
| Duy trì Admin Orders | `Views/Admin/Orders/` | ✅ Maintain |
| Admin Users Page | `Views/Admin/Users/` | 🆕 New |
| Admin Categories Page | `Views/Admin/Categories/` | 🆕 New |
| Admin Brands Page | `Views/Admin/Brands/` | 🆕 New |
| Dashboard Charts | Admin Dashboard + Chart.js | 🆕 New |
| Admin Sidebar | `Views/Shared/_AdminSidebar.cshtml` | 🔄 Upgrade |


## 👤 THÀNH VIÊN 5: Module Review, Blog & Infrastructure

### Backend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì Repository Pattern | `GuhaStore.Infrastructure/Repositories/` | ✅ Maintain |
| Duy trì UnitOfWork | `GuhaStore.Infrastructure/Repositories/UnitOfWork.cs` | ✅ Maintain |
| Duy trì DbContext | `GuhaStore.Infrastructure/Data/ApplicationDbContext.cs` | ✅ Maintain |
| Product Reviews CRUD | `Services/ReviewService.cs` | 🆕 New |
| Article/Blog Module | `Services/ArticleService.cs` | 🆕 New |
| File Upload improvements | `Services/FileUploadService.cs` | 🔄 Upgrade |
| Error Logging | `Middleware/ErrorLoggingMiddleware.cs` | 🆕 New |

### Frontend (50%)
| Công việc | File liên quan | Trạng thái |
|-----------|----------------|------------|
| Duy trì Layout | `Views/Shared/_Layout.cshtml` | ✅ Maintain |
| Duy trì Home Page | `Views/Home/Index.cshtml` | ✅ Maintain |
| Duy trì Contact Page | `Views/Contact/Index.cshtml` | ✅ Maintain |
| Product Review UI | `Views/Products/Details.cshtml` (review section) | 🆕 New |
| Blog List Page | `Views/Blog/Index.cshtml` | 🆕 New |
| Blog Detail Page | `Views/Blog/Details.cshtml` | 🆕 New |
| Footer improvements | `Views/Shared/_Layout.cshtml` | 🔄 Upgrade |
| 404/Error Pages | `Views/Shared/Error.cshtml` | 🔄 Upgrade |



