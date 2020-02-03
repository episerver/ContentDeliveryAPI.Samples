<template>
    <div class="CategoryPage">
        <nav class="Page-container PageHeader NavBar">
            <BackButton :prevUrl="model.parentLink.url" />
            <LanguageSelector :existingLanguages="model.existingLanguages" :language="model.language"></LanguageSelector>
        </nav>

        <div class="Page-container">
            <div class="top gutter">
                <h1 v-epi-edit="'Name'">{{model.name}}</h1>
            </div>
            <div class="list">
                <div v-for="(product, key) in products" :key="key">
                    <ProductCard :product-card="product" :key="key"></ProductCard>
                </div>
            </div>
            <div style="clear: both"></div>
        </div>

        <footer>
            <div class="FooterBottom">
                <h6>&copy; Music Festival 2018</h6>
            </div>
        </footer>
    </div>
</template>

<script>
    import api from '@/Scripts/api/api';
    import BackButton from '@/Scripts/components/widgets/BackButton.vue';
    import ProductCard from '@/Scripts/components/widgets/ProductCard.vue';
    import LanguageSelector from '@/Scripts/components/widgets/LanguageSelector.vue';

    export default {
        props: ['model'],
        data() {
            return {
                products: []
            };
        },
        mounted() {
            let categoryContentLink = this.model.contentLink.id + '__CatalogContent';

            api.getChildrenByContentLink(categoryContentLink, {}, { 'Accept-Language': '' })
                .then(success => {

                    let contentIds = '';
                    var products = [];
                    var getEntryContentPromises = [];
                    success.data.forEach((item, index) => {
                        if (item.contentType[0] === 'FestivalProduct') {
                            getEntryContentPromises.push(api.getChildrenByContentLink(item.contentLink.id + '__CatalogContent', {}, { 'Accept-Language': '' })
                                .then(entryContents => {
                                    item.variant = entryContents.data[0];
                                    products.push(item);
                                    contentIds += entryContents.data[0].contentLink.guidValue + ',';
                                }));
                        }
                    });

                    Promise.all(getEntryContentPromises)
                        .then(res => {
                            api.getPricing({
                                contentIds: contentIds,
                                marketId: 'default',
                                currencyCode: 'USD'
                            }).then(pricing => {
                                var pricings = pricing.data;

                                products.forEach((product, index) => {
                                    for (var i = 0; i < pricings.length; i++) {
                                        if (product.variant.code.value === pricings[i].entryCode) {
                                            if (pricings[i].discountedPrices.length) {
                                                product.defaultPrice = pricings[i].discountedPrices[0].defaultPrice;
                                                product.discountedPrice = pricings[i].discountedPrices.sort(function(a,b){return a.discountedPrice - b.discountedPrice})[0].discountedPrice;
                                            } else if (pricings[i].prices.length) {
                                                product.defaultPrice = pricings[i].prices[0].price;
                                            } else {
                                                product.priceNotFound = 'Price not found';
                                            }

                                            product.currencySymbol = "$";
                                            break;
                                        }
                                    }
                                });
                                this.products = products;
                            });
                        })
                });
        },
        components: {
            BackButton,
            ProductCard,
            LanguageSelector
        }
    };
</script>

<style lang="less" scoped>
    .top h1 {
        text-transform: none;
        margin: 0 40px 0 40px;
        padding: 0.3em 0;

        @media (min-width: 425px) {
            margin-right: 140px;
        }
    }

    h3 {
        text-transform: uppercase;
        width: 100%;
        text-align: center;
        background: rgba(236, 64, 122, .24);
        padding: 5px 0 7px;
        margin: 0;
    }
</style>
