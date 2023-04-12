FROM xueshanf/awscli

COPY ./* ./
RUN dos2unix setup-aws.sh
RUN chmod +x setup-aws.sh

CMD ["./setup-aws.sh"]