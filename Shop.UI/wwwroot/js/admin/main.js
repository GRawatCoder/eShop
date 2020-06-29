var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        products: [],
        productModel: {
            name: "Product Name",
            description: "Product Description",
            value: 150.99
        }
    },
    methods: {
        getProducts: function () {
            this.loading = true;
            axios.get('/Admin/products')
                .then(response => {
                    this.products = response.data;
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                });
        },
        createProducts: function () {
            this.loading = true;            
            axios.post('/Admin/products', this.productModel)
                .then(response => {
                    this.products.push(response.data);
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                })
        }
    },
    computed: {        
    }    
});