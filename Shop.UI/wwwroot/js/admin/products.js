var app = new Vue({
    el: '#app',
    data : {        
            loading: false,
            products: [],
            objectIndex: 0,
            editing: false,
            productModel: {
                id: 0,
                name: "Product Name",
                description: "Product Description",
                value: 150.99
            }        
    },
    mounted: function () {
        this.getProducts();
    },
    methods: {
        getProduct: function (id) {
            this.loading = true;
            axios.get('/Admin/products/' + id)
                .then(response => {
                    var product = response.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value
                    };
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                });
        },
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
                    this.editing = false;
                })
        },
        updateProducts: function () {
            this.loading = true;
            axios.put('/Admin/products', this.productModel)
                .then(response => {
                    this.products.splice(this.objectIndex, 1, response.data);
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                    this.editing = false;
                })
        },
        deleteProduct: function (id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id)
                .then(response => {
                    this.products.splice(index, 1);
                    console.log(response);
                }).catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                });
        },
        newProduct: function () {
            this.editing = true;
        },
        editProduct: function (id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.editing = true;

        },
        cancel: function () {
            this.editing = false;
        }
    },
    computed: {
    }
});