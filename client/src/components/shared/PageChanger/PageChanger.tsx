import React from 'react'
import Button from '../Button'

import styles from './PageChanger.module.scss'

type Props = {
    pageNumber: number
    onClickPrevious: () => void
    onClickNext: () => void
}

const PageChanger = ({ pageNumber, onClickPrevious, onClickNext }: Props) => {
    return (
        <div className={styles.container}>
            <Button onClick={onClickPrevious}>Previous</Button>
            <span>{`Page: ${pageNumber}`}</span>
            <Button onClick={onClickNext}>Next</Button>
        </div>
    )
}

export default PageChanger