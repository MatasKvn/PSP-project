import ItemCard from '@/components/shared/ItemCard'
import { Product } from '@/types/models'

type Props = {
    products: Product[]
    selectedProducts: Product[]
    isLoading: boolean
    isError: boolean
    className?: string
    style?: React.CSSProperties
    pageNumber: number
    onClick?: (productModifications: Product) => void
}

const ProductsView = (props: Props) => {
    const {
        products,
        isLoading,
        isError,
        className,
        style,
        selectedProducts,
        onClick
    } = props

    if (isLoading) {
        return <div className={className} style={style}>...Loading</div>
    }
    if (isError) {
        return <div className={className} style={style}>Error</div>
    }

    return (
        <div
            className={className}
            style={style}
        >
            {products.map((product) => (
                <div key={product.id} style={{ margin: '1em', display: 'inline-block' }}>
                    <ItemCard
                        type='product'
                        id={product.id}
                        description={product.description}
                        imageUrl={product.imageURL}
                        label={product.name}
                        price={product.price}
                        stock={product.stock}
                        isSelected={selectedProducts.some((selectedProduct) => selectedProduct.id === product.id)}
                        onClick={() => {
                            onClick?.(product)
                        }}

                    />
                </div>
            ))}
        </div>
    )
}

export default ProductsView