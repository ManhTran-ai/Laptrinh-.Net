// admin common js (placeholder)
document.addEventListener('DOMContentLoaded', () => {
    // simple confirm on delete buttons
    document.querySelectorAll('form[action*="DeleteProduct"] button[type="submit"]').forEach(btn => {
        btn.addEventListener('click', e => {
            if (!confirm('Bạn có chắc muốn xóa sản phẩm này?')) {
                e.preventDefault();
            }
        });
    });
});


