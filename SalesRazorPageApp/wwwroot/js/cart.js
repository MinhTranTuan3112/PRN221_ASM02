const baseUrl = `${window.location.protocol}//${window.location.host}`;

let orderId = Number(document.querySelector('input[name="orderId"]').value);

console.log(`Order id: ${orderId}`);

let deleteBtns = document.querySelectorAll('.delete_cart_btn');

deleteBtns.forEach(btn => {
    btn.addEventListener('click', async (e) => {
        e.preventDefault();
        const result = await Swal.fire({
            title: "Confirm remove this product from cart?",
            showCancelButton: true,
            confirmButtonText: "Confirm",
            icon: 'question'
        });

        if (!result.isConfirmed) {
            return;
        }

        let productId = Number(btn.getAttribute('product_id'));
        console.log(`Product id: ${productId}`);
        await updateCart(orderId, productId, 0);


    });
});

let confirmOrderBtn = document.querySelector('.confirm_order_btn');
confirmOrderBtn.addEventListener('click', async (e) => {
    e.preventDefault();
    const result = await Swal.fire({
        title: "Confirm Order?",
        showCancelButton: true,
        confirmButtonText: "Confirm",
        icon: 'question'
    });

    if (result.isConfirmed) {
        const response = await fetch(`${baseUrl}/api/orders/confirm-order`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        await Swal.fire("Order success!", "", "success");

        window.location.href = `/Index`;
    }
});

const updateCart = async (orderId, productId, quantity) => {
    try {
        const response = await fetch(`${baseUrl}/api/orders/update-cart`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                orderId: orderId,
                productId: productId,
                quantity: quantity
            })
        });
        if (response.status !== 204) {
            throw new Error('Failed to update cart');
        }


        console.log('Product removed from cart');
        await refreshCarts();

    } catch (error) {
        console.error(`Error updating cart: ${error}`);
    }
};

const refreshCarts = async () => {
    try {
        let memberId = Number(document.querySelector('.memberId').textContent);
        console.log(`Member id: ${memberId}`);
        const response = await fetch(`${baseUrl}/api/orders/cart-info/${memberId}`);
        if (response.status !== 200) {
            throw new Error('Failed to refresh cart');
        }

        const data = await response.json();
        console.log('Cart data:');
        console.log({ data });

        let cartContent = document.querySelector('.cart_content');
        cartContent.innerHTML = '';

        if (data.order.orderDetails === null || data.order.orderDetails.length === 0
            || data.order.orderDetails === undefined
        ) {
            let row = document.createElement('tr');
            row.textContent = 'The cart is empty';
            cartContent.appendChild(row);
            return;
        }

        let cnt = 1;

        data.order.orderDetails.forEach(od => {
            let row = document.createElement('tr');

            let cntCell = document.createElement('th');
            cntCell.textContent = cnt.toString();
            row.appendChild(cntCell);

            let deleteCartBtnCell = document.createElement('td');
            let deleteLink = document.createElement('a');
            deleteLink.className = 'text-danger delete_cart_btn';
            deleteLink.innerHTML = `<i class="ri-delete-bin-3-line"></i>`;

            deleteCartBtnCell.appendChild(deleteLink);
            row.appendChild(deleteCartBtnCell);

            let productNameCell = document.createElement('td');
            productNameCell.textContent = od.product.productName;
            row.appendChild(productNameCell);

            let quantityCell = document.createElement('td');
            let quantityDiv = document.createElement('div');
            quantityDiv.className = 'form-group mb-0';
            let quantityInput = document.createElement('input');
            quantityInput.type = 'number';
            quantityInput.className = 'form-control cart-qty';
            quantityInput.setAttribute('product_id', od.product.productId);
            quantityInput.name = `cartQty${cnt}`;
            quantityInput.id = `cartQty${cnt}`;
            quantityInput.value = od.quantity;
            quantityDiv.appendChild(quantityInput);
            quantityCell.appendChild(quantityDiv);
            row.appendChild(quantityCell);

            let unitPriceCell = document.createElement('td');
            unitPriceCell.textContent = `$${od.product.unitPrice}`;
            row.appendChild(unitPriceCell);

            let totalPriceCell = document.createElement('td');
            totalPriceCell.className = 'text-right';
            totalPriceCell.textContent = `$${od.unitPrice}`;
            row.appendChild(totalPriceCell);


            cartContent.appendChild(row);
            ++cnt;

        });

        document.querySelector('.order_total_content').textContent = `$${data.order.freight}`;


    } catch (error) {
        console.error(`Error refreshing cart: ${error}`);
    }
};