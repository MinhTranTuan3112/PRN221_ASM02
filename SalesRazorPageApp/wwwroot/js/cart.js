const baseUrl = `${window.location.protocol}//${window.location.host}`;

let orderId = Number(document.querySelector('input[name="orderId"]').value);

console.log(`Order id: ${orderId}`);

let deleteBtns = document.querySelectorAll('.delete_cart_btn');

deleteBtns.forEach(btn => {
    btn.addEventListener('click', async () => {
        let productId = Number(btn.getAttribute('product_id'));
        console.log(`Product id: ${productId}`);
        const response = await fetch(`${baseUrl}/api/orders/update-cart`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                orderId: orderId,
                productId: productId,
                quantity: 0
            })
        });

        if (response.status === 204) {
            console.log('Product removed from cart');
            window.location.reload();
        }
    });
});

let confirmOrderBtn = document.querySelector('.confirm_order_btn');
confirmOrderBtn.addEventListener('click', async () => {
    const result = await Swal.fire({
        title: "Confirm Order?",
        showCancelButton: true,
        confirmButtonText: "Confirm",
    });

    if (result.isConfirmed) {
        const response = await fetch(`${baseUrl}/api/orders/confirm-order`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        Swal.fire("Order success!", "", "success");
    }
});