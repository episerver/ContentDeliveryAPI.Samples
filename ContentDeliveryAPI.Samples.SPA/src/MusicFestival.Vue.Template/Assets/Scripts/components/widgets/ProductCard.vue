<template>
    <epi-link :url="productCard.url">
        <div class="product-card">
            <ConditionalImage :src="productCard.assets[0]" :alt="productCard.name"></ConditionalImage>
            <h4>{{productCard.name}}</h4>
            <div>
                <h4 :class="{ hasDiscount: productCard.discountedPrice != null }" v-show="productCard.priceNotFound == null">{{productCard.currencySymbol}}{{productCard.defaultPrice}}</h4>
                <h4 style="color:red;" v-show="productCard.priceNotFound != null">{{productCard.priceNotFound}}</h4>
                <h4 style="color:red;" v-show="productCard.discountedPrice != null">{{productCard.currencySymbol}}{{productCard.discountedPrice}}</h4>
            </div>
            <button style="width: 100%"
                type="submit"
                role="button"
                class="btn btn-primary jsAddToCart"
                data-container="MiniCart"
            >
                <span class="glyphicon glyphicon-shopping-cart"></span> Add to cart
            </button>
        </div>
    </epi-link>
</template>

<script>
import ConditionalImage from '@/Scripts/components/widgets/ConditionalImage.vue';
import EpiLink from '@/Scripts/components/widgets/EpiLink.vue';

export default {
    props: ['productCard'],
    data() {
        return {
            variant: {}
        };
    },
    computed: {
    },
    components: {
        ConditionalImage,
        EpiLink
    }
};
</script>

<style lang="less" scoped>
    @import "../../../styles/common/variables.less";

    .hasDiscount {
    text-decoration: line-through;
    }

    .product-card{
        float: left;
        width: 28%;
        margin: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        box-shadow: 1px 1px 3px rgba(0,0,0, 0.5);
        padding: 5px;
    }

    .product-card img {
        width: 100%;
        max-height: 320px;
    }

    .round{
        width: 40px;
        height: 40px;
        border-radius: 40px;
        overflow:hidden;
        margin-right:10px;
        img{
            max-width:150%;
            margin-left:-15px;
        }

        &.big{
            width: 60px;
            height: 60px;
        }
    }
    .info{
        flex-grow: 1;
        p{
            margin:0;
            font:14px/20px @fontSubHeading;
        }
        span{
            display: block;
            font: 12px/16px @fontFamilyPrimary;
        }
    }

    .product-price--discount {
        color: green;
    }

    .hasDiscount {
        text-decoration: line-through;
    }
</style>
