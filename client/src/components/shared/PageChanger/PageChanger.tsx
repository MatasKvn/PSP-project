import Button from '../Button'

type Props = {
    pageNumber: number
    onClickPrevious: () => void
    onClickNext: () => void
}

const PageChanger = ({ pageNumber, onClickPrevious, onClickNext }: Props) => {
    return (
        <div>
            <Button onClick={onClickPrevious}>Previous</Button>
            <span>{`Page: ${pageNumber}`}</span>
            <Button onClick={onClickNext}>Next</Button>
        </div>
    )
}

export default PageChanger