namespace Shared.Common
{
    public enum CategoryCode
    {
        PROPERTY_TYPE,
        SUBSCRIPTION_STATUS,
    }

    public enum UnitStatus
    {
        AVAILABLE = 1,       // ĐANG TRỐNG
        OCCUPIED = 2,        // ĐANG Ở
        NOTICE_GIVEN = 3,    // ĐANG BÁO KẾT THÚC
        RESERVED = 4,        // ĐANG CỌC GIỮ CHỖ
        MAINTENANCE = 5,     // ĐANG SỬA CHỮA
        DRAFT = 0            // ĐANG KHỞI TẠO (Dành cho Super Admin setup)
    }
    public enum ContractAlertStatus
    {
        EXPIRING_SOON = 1,   // SẮP HẾT HẠN HỢP ĐỒNG (VD: còn < 30 ngày)
        EXPIRED = 2,         // ĐÃ QUÁ HẠN HỢP ĐỒNG
        ACTIVE = 3           // HỢP ĐỒNG CÒN HẠN
    }

    public enum PaymentStatus
    {
        PAID = 1,            // ĐÃ THANH TOÁN
        DEBT = 2,            // ĐANG NỢ TIỀN
        PARTIAL = 3          // THANH TOÁN MỘT PHẦN
    }
}
