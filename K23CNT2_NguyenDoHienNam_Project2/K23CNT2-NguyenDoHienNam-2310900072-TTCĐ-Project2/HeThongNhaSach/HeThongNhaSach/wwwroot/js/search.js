document.addEventListener("DOMContentLoaded", function () {
    const searchBox = document.getElementById("searchBox");
    const suggestions = document.getElementById("suggestions");

    if (!searchBox) {
        console.error("❌ Không tìm thấy searchBox");
        return;
    }

    searchBox.addEventListener("input", function () {
        const query = this.value.trim();
        console.log("Gõ:", query);

        if (query.length < 2) {
            suggestions.innerHTML = "";
            return;
        }

        fetch(`/SanPham/Search?term=${encodeURIComponent(query)}`)
            .then(res => res.json())
            .then(data => {
                console.log("Kết quả API:", data);
                suggestions.innerHTML = "";
                data.forEach(item => {
                    const li = document.createElement("li");
                    li.classList.add("list-group-item", "list-group-item-action");
                    li.textContent = `${item.tenSp} - ${item.giaBan.toLocaleString()} đ`;
                    li.onclick = () => {
                        window.location.href = `/SanPham/Details/${item.maSp}`;
                    };
                    suggestions.appendChild(li);
                });
            })
            .catch(err => console.error("Lỗi fetch:", err));
    });
});
