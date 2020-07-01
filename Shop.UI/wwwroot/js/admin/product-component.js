Vue.component('product-manager', {
    template: ` <div v-if="!editing">
                    <button class="button is-rounded" @click="newProduct">Add New Product</button>
                    <table class="table">
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Value</th>
                        </tr>
                        <tr v-for="(product,index) in products">
                            <td>{{product.id}}</td>
                            <td>{{product.name}}</td>
                            <td>{{product.value}}</td>
                            <td><a @click="editProduct(product.id,index)">Edit</a></td>
                            <td><a @click="deleteProduct(product.id,index)">Remove</a></td>                            
                        </tr>
                    </table>                    
                </div>

                <div v-else>
                    <div class="field">
                        <label class="label">Product Name</label>
                        <div class="control">
                            <input class="input is-rounded" v-model="productModel.name" />
                        </div>
                    </div>
                    <div class="field">
                        <label class="label">Product Description</label>
                        <div class="control">
                            <input class="input is-rounded" v-model="productModel.description" />
                        </div>
                    </div>
                    <div class="field">
                        <label class="label">Product Value</label>
                        <div class="control">
                            <input class="input is-rounded" type="number" v-model.number="productModel.value" />
                        </div>
                    </div>
                    <button class="button is-success is-rounded" v-on:click="createProducts" v-if="!productModel.id">Create Products</button>
                    <button class="button is-warning is-rounded" v-on:click="updateProducts" v-else>Update Product</button>
                    <button class="button is-danger is-rounded" v-on:click="cancel">Cancel</button>
                </div>`,
    data() {
        return {
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