const baseUrl = `${window.location.protocol}//${window.location.host}`;

let confirmAddToCartBtn = document.querySelector('.confirm_add_to_cart_btn');

let addToCartBtns = document.querySelectorAll('.add_to_cart_btn');

addToCartBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        let productId = Number(btn.getAttribute('product_id'));
        console.log(`Clicked add to cart button for product id: ${productId}`);
        confirmAddToCartBtn.setAttribute('product_id', productId);
    });
});

confirmAddToCartBtn.addEventListener('click', async () => {
    console.log('Clicked add to cart button');
    let memberId = document.querySelector('.memberId').textContent;

    if (!memberId || memberId === '') {
        window.location.href = '/Login';
        return;
    }

    let productId = Number(confirmAddToCartBtn.getAttribute('product_id'));
    let quantity = Number(document.querySelector('#cartQuantity').value);

    console.log(`Member id: ${memberId}, Product id: ${productId}, Quantity: ${quantity}`);

    const response = await fetch(`${baseUrl}/api/orders/add-to-cart`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            memberId: memberId,
            productId: productId,
            quantity: quantity
        })
    });

    if (response.ok) {
        console.log(`Added product with id ${productId} to cart`);
        Swal.fire({
            title: "SUCCESS!",
            text: "Added this product to cart!",
            icon: "success"
        });
    } else {
        console.error('Failed to add product to cart');
    }

});