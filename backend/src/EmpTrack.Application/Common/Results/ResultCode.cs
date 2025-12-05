namespace EmpTrack.Application.Common.Results
{
    public enum ResultCode
    {
        Success = 200,         // HTTP 200 OK → İşlem başarıyla tamamlandı.
        Created = 201,         // HTTP 201 Created → Yeni kayıt başarıyla oluşturuldu.
        NoContent = 204,       // HTTP 204 NoContent → Güncelleme veya silme sonrası cevap gövdesi dönülmez.

        BadRequest = 400,     // HTTP 400 BadRequest → İstek geçersiz, validation hatası var.
        Unauthorized = 401,   // HTTP 401 Unauthorized → Authentication başarısız, yetkilendirme yok.
        NotFound = 404,       // HTTP 404 NotFound → İstenen kayıt sistemde bulunamadı.
        Conflict = 409,       // HTTP 409 Conflict → Mevcut kayıtlarla çakışma var, işlem yapılamaz. (RegistrationNumber gibi unique alanlar için)

        InternalError = 500   // HTTP 500 InternalServerError → Sunucu tarafında beklenmeyen bir hata oluştu.
    }
}
