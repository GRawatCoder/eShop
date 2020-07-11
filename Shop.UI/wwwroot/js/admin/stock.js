var app = new Vue({
    el: '#app',        
    data: {
        products: [],
        selectedProduct: null,
        newStock: {
            productId: 0,
            description: 'Size',
            qty:10
        }
    },
    mounted: function() {
        this.getStock();
    },
    methods: {
        getStock: function () {
            this.loading = true;
            axios.get('/Admin/stocks')
                .then(response => {
                    this.products = response.data;
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                });
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;
        },
        updateStock() {
            this.loading = true;
            axios.put('/Admin/stocks', this.selectedProduct)
                .then(response => {
                    this.selectedProduct.stocks =  response.data.stocks;
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;                   
                })
        },
        addStock() {
            this.loading = true;
            axios.post('/Admin/stocks', this.newStock)
                .then(response => {
                    this.selectedProduct.stocks.push(response.data);
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;                    
                })
        },
        deleteProduct(id,index) {
            this.loading = true;
            axios.delete('/Admin/stocks/' + id)
                .then(response => {
                    this.products.stocks.splice(index, 1);
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                });
        }
    }
});