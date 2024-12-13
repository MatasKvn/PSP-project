import ItemCard from '@/components/shared/ItemCard'
import { Service } from '@/types/models'

type Props = {
    services: Service[]
    selectedServices: Service[]
    isLoading: boolean
    isError: boolean
    className?: string
    style?: React.CSSProperties
    pageNumber: number
    onClick?: (productModifications: Service) => void
}

const ServicesView = (props: Props) => {
    const {
        services,
        isLoading,
        isError,
        className,
        style,
        selectedServices,
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
            {services.map((service) => (
                <div key={service.id} style={{ margin: '1em', display: 'inline-block' }}>
                    <ItemCard
                        type='service'
                        id={service.id}
                        description={service.description}
                        imageUrl={service.imageUrl}
                        label={service.name}
                        price={service.price}
                        isSelected={selectedServices.some((selectedServices) => selectedServices.id === service.id)}
                        onClick={() => {
                            onClick?.(service)
                        }}

                    />
                </div>
            ))}
        </div>
    )
}

export default ServicesView