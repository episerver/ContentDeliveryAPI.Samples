<template>
    <div class="ProductNode">
        <nav class="Page-container PageHeader NavBar">
            <BackButton :prevUrl="model.parentLink.url" />
            <LanguageSelector :existingLanguages="model.existingLanguages"
                              :language="model.language"></LanguageSelector>
        </nav>

        <div class="Page-container u-posRelative" style="padding-top: 80px;">
            <div style="float: left; width: 30%">
                <ProductImageCarousel v-bind:images="model.assets" />
            </div>
            <div style="float: right; width: 68%">
                <div>
                    <h1 v-epi-edit="'DisplayName'">{{model.displayName.value}}</h1>
                </div>

                <div class="col-xs-12 col-sm-12" style="margin-top: 15px;">
                    <h4 class="product-brand">{{model.brand.value}}</h4>
                </div>

                <div class="row" style="margin-top: 15px;">
                    <div class="col-xs-12">
                        <div v-html="model.longDescription.value"></div>
                        <p v-html="model.description.value"></p>
                        <p>
                            <strong>SKU:</strong>
                            {{variant.code != null ? variant.code.value : ''}}
                        </p>
                        <div>
                            <h4 :class="{ hasDiscount: discountedPrice != null }" v-show="priceNotFound == null">{{currencySymbol}}{{defaultPrice}}</h4>
                            <h4 style="color:red;" v-show="priceNotFound != null">{{priceNotFound}}</h4>
                            <h4 style="color:red;" v-show="discountedPrice != null">{{currencySymbol}}{{discountedPrice}}</h4>
                        </div>
                        <div>
                            <p><strong>The product is available in the following warehouse:</strong></p>
                            <div v-for="inventory in inventories" :key="inventory.warehouseCode">
                                <button>
                                    <p><small>Warehouse: {{inventory.warehouse.name}}</small></p>
                                    <p><small>Available Quantity: {{inventory.purchaseAvailableQuantity}}</small></p>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <br>
                <br>
                <div class="row">
                    <div class="col-md-7">
                        <button type="submit"
                                role="button"
                                class="btn btn-primary jsAddToCart"
                                data-container="MiniCart">
                            <span class="glyphicon glyphicon-shopping-cart"></span> Add to cart
                        </button>

                        <button type="submit"
                                role="button"
                                class="btn btn-default btn--blue jsAddToCart"
                                data-container="WishListMiniCart">
                            <span class="glyphicon glyphicon-heart"></span> Add to wishlist
                        </button>
                    </div>
                    <div id="CartWarningMessage"
                         class="col-md-5 col-xs-12 alert alert-danger pull-right"
                         hidden>
                        <button type="button" class="close" data-hide="alert">&times;</button>
                        <span class="warning-message"></span>
                    </div>
                </div>
            </div>
            <div style="clear: both"></div>
        </div>
    </div>
</template>

<script>
    import api from '@/Scripts/api/api';
    import ProductImageCarousel from '@/Scripts/components/widgets/ProductImageCarousel.vue';
    import BackButton from '@/Scripts/components/widgets/BackButton.vue';
    import LanguageSelector from '@/Scripts/components/widgets/LanguageSelector.vue';
    import EpiProperty from '@/Scripts/components/EpiProperty.vue';

    export default {
        props: ['model'],
        data() {
            return {
                variant: {},
                inventories: [],
                defaultPrice: null,
                discountedPrice: null,
                priceNotFound: null,
                currencySymbol: null
            };
        },
        mounted() {
            api.getChildrenByContentLink(this.model.contentLink.id + '__CatalogContent', {}, { 'Accept-Language': '' })
                .then(res => {
                    let variants = res.data;
                    if (variants != null && variants.length > 0) {
                        this.variant = variants[0];
                        api.getInventory({ contentId: variants[0].contentLink.guidValue }).then(inventory => {
                            inventory.data.forEach(inventory => {
                                api.getWarehouse(inventory.warehouseCode).then(warehouse => {
                                    inventory.warehouse = warehouse.data;
                                    this.inventories.push(inventory);
                                });
                            });
                        });

                        api.getPricing({
                            contentIds: variants[0].contentLink.guidValue,
                            marketId: 'default',
                            currencyCode: 'usd'
                        }).then(pricing => {
                            if (pricing.data[0].discountedPrices.length) {
                                this.defaultPrice = pricing.data[0].discountedPrices[0].defaultPrice;
                                this.discountedPrice = pricing.data[0].discountedPrices[0].discountedPrice;
                            } else if (pricing.data[0].prices.length) {
                                this.defaultPrice = pricing.data[0].prices[0].price;
                            } else {
                                this.priceNotFound = 'Price not found';
                            }

                            this.currencySymbol = "$";
                        });
                    }
                });
        },
        computed: {
        },
        methods: {
            friendlyDateTime(dateTime) {
                return new Date(dateTime).toLocaleString();
            }
        },
        components: {
            ProductImageCarousel,
            BackButton,
            LanguageSelector,
            EpiProperty
        }
    };
</script>

<style lang="less" scoped>
    @import "../../../Styles/Common/variables.less";

    @footerHeight: 46px;

    .hasDiscount {
        text-decoration: line-through;
    }

    .ProductNode {
        position: relative;
        padding-bottom: @footerHeight;
    }

    .top {
        text-align: center;
        position: absolute;
        width: 100%;
        h1

    {
        position: relative;
        font-size: 1em;
        top: -2.5em;
    }

    }

    .FooterBottom {
        position: absolute;
        bottom: 0;
        left: 0;
        right: 0;
        height: @footerHeight;
    }

    .product-price--discount {
        color: green;
    }

    .hasDiscount {
        text-decoration: line-through;
    }

    @media (min-width: 768px) {
        .top {
            h1

    {
        font-size: 2em;
        top: -2em;
    }

    }
    }

    @media (min-width: 1224px) {
        .top {
            h1

    {
        font-size: 2.5em;
        top: -1.5em;
    }

    }
    }
</style>
