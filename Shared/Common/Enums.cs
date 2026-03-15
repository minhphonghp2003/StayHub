namespace Shared.Common
{
    public enum CategoryCode
    {
        PROPERTY_TYPE,
        SUBSCRIPTION_STATUS,
    }

    public enum UnitStatus
    {
        AVAILABLE,       // ĐANG TRỐNG
        OCCUPIED,        // ĐANG Ở
        NOTICE_GIVEN,    // ĐANG BÁO KẾT THÚC
        RESERVED,        // ĐANG CỌC GIỮ CHỖ
        MAINTENANCE,     // ĐANG SỬA CHỮA
        DRAFT            // ĐANG KHỞI TẠO (Dành cho Super Admin setup)
    }
    public enum ContractAlertStatus
    {
        EXPIRING_SOON,   // SẮP HẾT HẠN HỢP ĐỒNG (VD: còn < 30 ngày)
        EXPIRED,         // ĐÃ QUÁ HẠN HỢP ĐỒNG
        ACTIVE           // HỢP ĐỒNG CÒN HẠN
    }

    public enum PaymentStatus
    {
        PAID,            // ĐÃ THANH TOÁN
        DEBT,            // ĐANG NỢ TIỀN
        PARTIAL          // THANH TOÁN MỘT PHẦN
    }
    public enum SystemRole
    {
        SUPER_ADMIN,
        SUPPORT,
        STAFF
    }
    public enum ContractStatus
    {
        Pending,        // Chờ duyệt (Chờ quản lý hoặc chủ nhà xác nhận)
        Active,         // Đang hiệu lực (Khách đang ở và đóng tiền)
        ExpiringSoon,   // Sắp hết hạn (Thường là trước 30 ngày)
        Expired,        // Đã hết hạn (Chưa làm thủ tục thanh lý)
        Terminated,     // Đã thanh lý (Kết thúc hợp đồng đúng hạn hoặc trước hạn)
        Canceled,       // Đã hủy (Hợp đồng bị hủy trước khi bắt đầu)
    }

    public enum InvoiceStatus
    {
        Pending, Active, Expired
    }
}
