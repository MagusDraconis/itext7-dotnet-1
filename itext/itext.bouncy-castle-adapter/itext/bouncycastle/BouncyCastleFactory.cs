using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Esf;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.X509;
using iText.Bouncycastle.Asn1;
using iText.Bouncycastle.Asn1.Cms;
using iText.Bouncycastle.Asn1.Esf;
using iText.Bouncycastle.Asn1.Ess;
using iText.Bouncycastle.Asn1.Ocsp;
using iText.Bouncycastle.Asn1.Pcks;
using iText.Bouncycastle.Asn1.Tsp;
using iText.Bouncycastle.Asn1.Util;
using iText.Bouncycastle.Asn1.X500;
using iText.Bouncycastle.Asn1.X509;
using iText.Bouncycastle.Cert;
using iText.Bouncycastle.Cert.Jcajce;
using iText.Bouncycastle.Cert.Ocsp;
using iText.Bouncycastle.Cms;
using iText.Bouncycastle.Crypto;
using iText.Bouncycastle.Crypto.Generators;
using iText.Bouncycastle.Math;
using iText.Bouncycastle.Openssl;
using iText.Bouncycastle.Operator;
using iText.Bouncycastle.Security;
using iText.Bouncycastle.Tsp;
using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle;
using iText.Commons.Bouncycastle.Asn1;
using iText.Commons.Bouncycastle.Asn1.Cms;
using iText.Commons.Bouncycastle.Asn1.Esf;
using iText.Commons.Bouncycastle.Asn1.Ess;
using iText.Commons.Bouncycastle.Asn1.Ocsp;
using iText.Commons.Bouncycastle.Asn1.Pkcs;
using iText.Commons.Bouncycastle.Asn1.Tsp;
using iText.Commons.Bouncycastle.Asn1.Util;
using iText.Commons.Bouncycastle.Asn1.X500;
using iText.Commons.Bouncycastle.Asn1.X509;
using iText.Commons.Bouncycastle.Cert;
using iText.Commons.Bouncycastle.Cert.Jcajce;
using iText.Commons.Bouncycastle.Cert.Ocsp;
using iText.Commons.Bouncycastle.Cms;
using iText.Commons.Bouncycastle.Crypto;
using iText.Commons.Bouncycastle.Crypto.Generators;
using iText.Commons.Bouncycastle.Math;
using iText.Commons.Bouncycastle.Openssl;
using iText.Commons.Bouncycastle.Operator;
using iText.Commons.Bouncycastle.Security;
using iText.Commons.Bouncycastle.Tsp;
using iText.Commons.Bouncycastle.X509;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using ICertificateID = iText.Commons.Bouncycastle.Cert.Ocsp.ICertificateID;
using ICipher = iText.Commons.Bouncycastle.Crypto.ICipher;
using ISingleResp = iText.Commons.Bouncycastle.Cert.Ocsp.ISingleResp;

namespace iText.Bouncycastle {
    /// <summary>
    /// This class implements
    /// <see cref="iText.Commons.Bouncycastle.IBouncyCastleFactory"/>
    /// and creates bouncy-castle classes instances.
    /// </summary>
    public class BouncyCastleFactory : IBouncyCastleFactory {
        private static readonly String PROVIDER_NAME = "BC";
        private static readonly BouncyCastleTestConstantsFactory BOUNCY_CASTLE_TEST_CONSTANTS = new BouncyCastleTestConstantsFactory();

        public virtual IASN1ObjectIdentifier CreateASN1ObjectIdentifier(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerObjectIdentifier) {
                return new ASN1ObjectIdentifierBC((DerObjectIdentifier)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1ObjectIdentifier CreateASN1ObjectIdentifier(String str) {
            return new ASN1ObjectIdentifierBC(str);
        }

        public virtual IASN1ObjectIdentifier CreateASN1ObjectIdentifierInstance(Object @object) {
            return new ASN1ObjectIdentifierBC(DerObjectIdentifier.GetInstance(@object is ASN1EncodableBC ? ((ASN1EncodableBC
                )@object).GetEncodable() : @object));
        }

        public virtual IASN1InputStream CreateASN1InputStream(Stream stream) {
            return new ASN1InputStreamBC(stream);
        }

        public virtual IASN1InputStream CreateASN1InputStream(byte[] bytes) {
            return new ASN1InputStreamBC(bytes);
        }

        public virtual IASN1OctetString CreateASN1OctetString(IASN1Primitive primitive) {
            ASN1PrimitiveBC primitiveBC = (ASN1PrimitiveBC)primitive;
            if (primitiveBC.GetPrimitive() is Asn1OctetString) {
                return new ASN1OctetStringBC((Asn1OctetString)primitiveBC.GetPrimitive());
            }
            return null;
        }

        public virtual IASN1OctetString CreateASN1OctetString(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is Asn1OctetString) {
                return new ASN1OctetStringBC((Asn1OctetString)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1OctetString CreateASN1OctetString(IASN1TaggedObject taggedObject, bool b) {
            return new ASN1OctetStringBC(taggedObject, b);
        }

        public virtual IASN1OctetString CreateASN1OctetString(byte[] bytes) {
            return new ASN1OctetStringBC(Asn1OctetString.GetInstance(bytes));
        }

        public virtual IASN1Sequence CreateASN1Sequence(Object @object) {
            if (@object is Asn1Sequence) {
                return new ASN1SequenceBC((Asn1Sequence)@object);
            }
            return null;
        }

        public virtual IASN1Sequence CreateASN1Sequence(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is Asn1Sequence) {
                return new ASN1SequenceBC((Asn1Sequence)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1Sequence CreateASN1Sequence(byte[] array) {
            return new ASN1SequenceBC((Asn1Sequence)Asn1Sequence.FromByteArray(array));
        }

        public virtual IASN1Sequence CreateASN1SequenceInstance(Object @object) {
            return new ASN1SequenceBC(@object is ASN1EncodableBC ? ((ASN1EncodableBC)@object).GetEncodable() : @object
                );
        }

        public virtual IDERSequence CreateDERSequence(IASN1EncodableVector encodableVector) {
            ASN1EncodableVectorBC vectorBC = (ASN1EncodableVectorBC)encodableVector;
            return new DERSequenceBC(vectorBC.GetEncodableVector());
        }

        public virtual IDERSequence CreateDERSequence(IASN1Primitive primitive) {
            ASN1PrimitiveBC primitiveBC = (ASN1PrimitiveBC)primitive;
            return new DERSequenceBC(primitiveBC.GetPrimitive());
        }

        public virtual IASN1TaggedObject CreateASN1TaggedObject(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is Asn1TaggedObject) {
                return new ASN1TaggedObjectBC((Asn1TaggedObject)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1Integer CreateASN1Integer(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerInteger) {
                return new ASN1IntegerBC((DerInteger)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1Integer CreateASN1Integer(int i) {
            return new ASN1IntegerBC(i);
        }

        public virtual IASN1Integer CreateASN1Integer(IBigInteger i) {
            return new ASN1IntegerBC(i);
        }

        public virtual IASN1Set CreateASN1Set(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is Asn1Set) {
                return new ASN1SetBC((Asn1Set)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1Set CreateASN1Set(Object encodable) {
            return encodable is Asn1Set ? new ASN1SetBC((Asn1Set)encodable) : null;
        }

        public virtual IASN1Set CreateASN1Set(IASN1TaggedObject taggedObject, bool b) {
            ASN1TaggedObjectBC taggedObjectBC = (ASN1TaggedObjectBC)taggedObject;
            return new ASN1SetBC(taggedObjectBC.GetASN1TaggedObject(), b);
        }

        public virtual IASN1Set CreateNullASN1Set() {
            return new ASN1SetBC(null);
        }

        public virtual IASN1OutputStream CreateASN1OutputStream(Stream stream) {
            return new ASN1OutputStreamBC(stream);
        }

        public virtual IASN1OutputStream CreateASN1OutputStream(Stream outputStream, String asn1Encoding) {
            if (Asn1Encodable.Ber.Equals(asn1Encoding)) {
                return new ASN1OutputStreamBC(new BerOutputStream(outputStream));
            }
            return new ASN1OutputStreamBC(new DerOutputStream(outputStream));
        }

        public virtual IDEROctetString CreateDEROctetString(byte[] bytes) {
            return new DEROctetStringBC(bytes);
        }

        public virtual IDEROctetString CreateDEROctetString(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerOctetString) {
                return new DEROctetStringBC((DerOctetString)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1EncodableVector CreateASN1EncodableVector() {
            return new ASN1EncodableVectorBC();
        }

        public virtual IDERNull CreateDERNull() {
            return DERNullBC.INSTANCE;
        }

        public virtual IDERTaggedObject CreateDERTaggedObject(int i, IASN1Primitive primitive) {
            ASN1PrimitiveBC primitiveBC = (ASN1PrimitiveBC)primitive;
            return new DERTaggedObjectBC(i, primitiveBC.GetPrimitive());
        }

        public virtual IDERTaggedObject CreateDERTaggedObject(bool b, int i, IASN1Primitive primitive) {
            ASN1PrimitiveBC primitiveBC = (ASN1PrimitiveBC)primitive;
            return new DERTaggedObjectBC(b, i, primitiveBC.GetPrimitive());
        }

        public virtual IDERSet CreateDERSet(IASN1EncodableVector encodableVector) {
            ASN1EncodableVectorBC encodableVectorBC = (ASN1EncodableVectorBC)encodableVector;
            return new DERSetBC(encodableVectorBC.GetEncodableVector());
        }

        public virtual IDERSet CreateDERSet(IASN1Primitive primitive) {
            ASN1PrimitiveBC primitiveBC = (ASN1PrimitiveBC)primitive;
            return new DERSetBC(primitiveBC.GetPrimitive());
        }

        public virtual IDERSet CreateDERSet(ISignaturePolicyIdentifier identifier) {
            SignaturePolicyIdentifierBC identifierBC = (SignaturePolicyIdentifierBC)identifier;
            return new DERSetBC(identifierBC.GetSignaturePolicyIdentifier());
        }

        public virtual IDERSet CreateDERSet(IRecipientInfo recipientInfo) {
            RecipientInfoBC recipientInfoBC = (RecipientInfoBC)recipientInfo;
            return new DERSetBC(recipientInfoBC.GetRecipientInfo());
        }

        public virtual IASN1Enumerated CreateASN1Enumerated(int i) {
            return new ASN1EnumeratedBC(i);
        }

        public virtual IASN1Encoding CreateASN1Encoding() {
            return ASN1EncodingBC.GetInstance();
        }

        public virtual IAttributeTable CreateAttributeTable(IASN1Set unat) {
            ASN1SetBC asn1SetBC = (ASN1SetBC)unat;
            return new AttributeTableBC(asn1SetBC.GetASN1Set());
        }

        public virtual IPKCSObjectIdentifiers CreatePKCSObjectIdentifiers() {
            return PKCSObjectIdentifiersBC.GetInstance();
        }

        public virtual IAttribute CreateAttribute(IASN1ObjectIdentifier attrType, IASN1Set attrValues) {
            ASN1ObjectIdentifierBC attrTypeBc = (ASN1ObjectIdentifierBC)attrType;
            ASN1SetBC attrValuesBc = (ASN1SetBC)attrValues;
            return new AttributeBC(new Org.BouncyCastle.Asn1.Cms.Attribute(attrTypeBc.GetASN1ObjectIdentifier(), attrValuesBc
                .GetASN1Set()));
        }

        public virtual IContentInfo CreateContentInfo(IASN1Sequence sequence) {
            ASN1SequenceBC sequenceBC = (ASN1SequenceBC)sequence;
            return new ContentInfoBC(ContentInfo.GetInstance(sequenceBC.GetASN1Sequence()));
        }

        public virtual IContentInfo CreateContentInfo(IASN1ObjectIdentifier objectIdentifier, IASN1Encodable encodable
            ) {
            return new ContentInfoBC(objectIdentifier, encodable);
        }

        public virtual ISigningCertificate CreateSigningCertificate(IASN1Sequence sequence) {
            ASN1SequenceBC sequenceBC = (ASN1SequenceBC)sequence;
            return new SigningCertificateBC(SigningCertificate.GetInstance(sequenceBC.GetASN1Sequence()));
        }

        public virtual ISigningCertificateV2 CreateSigningCertificateV2(IASN1Sequence sequence) {
            ASN1SequenceBC sequenceBC = (ASN1SequenceBC)sequence;
            return new SigningCertificateV2BC(SigningCertificateV2.GetInstance(sequenceBC.GetASN1Sequence()));
        }

        public virtual IBasicOCSPResponse CreateBasicOCSPResponse(IASN1Primitive primitive) {
            ASN1PrimitiveBC primitiveBC = (ASN1PrimitiveBC)primitive;
            return new BasicOCSPResponseBC(BasicOcspResponse.GetInstance(primitiveBC.GetPrimitive()));
        }

        public IBasicOCSPResponse CreateBasicOCSPResponse(object response) {
            if (response is BasicOcspResponse) {
                return new BasicOCSPResponseBC((BasicOcspResponse) response);
            }
            return null;
            
        }

        public virtual IOCSPObjectIdentifiers CreateOCSPObjectIdentifiers() {
            return OCSPObjectIdentifiersBC.GetInstance();
        }

        public virtual IAlgorithmIdentifier CreateAlgorithmIdentifier(IASN1ObjectIdentifier algorithm) {
            ASN1ObjectIdentifierBC algorithmBc = (ASN1ObjectIdentifierBC)algorithm;
            return new AlgorithmIdentifierBC(new AlgorithmIdentifier(algorithmBc.GetASN1ObjectIdentifier(), null));
        }

        public virtual IAlgorithmIdentifier CreateAlgorithmIdentifier(IASN1ObjectIdentifier algorithm, IASN1Encodable
             encodable) {
            ASN1ObjectIdentifierBC algorithmBc = (ASN1ObjectIdentifierBC)algorithm;
            ASN1EncodableBC encodableBc = (ASN1EncodableBC)encodable;
            return new AlgorithmIdentifierBC(new AlgorithmIdentifier(algorithmBc.GetASN1ObjectIdentifier(), encodableBc
                .GetEncodable()));
        }

        public virtual String GetProviderName() {
            return PROVIDER_NAME;
        }

        public virtual ICertificateID CreateCertificateID() {
            return CertificateIDBC.GetInstance();
        }

        public virtual IExtensions CreateExtensions(IASN1ObjectIdentifier objectIdentifier, bool critical, IASN1OctetString
             octetString) {
            IDictionary extension = new Hashtable();
            extension.Add(((ASN1ObjectIdentifierBC)objectIdentifier).GetASN1ObjectIdentifier(),
                new X509Extension(critical, ((ASN1OctetStringBC)octetString).GetASN1OctetString()));
            return new ExtensionsBC(new X509Extensions(extension));
        }

        public IExtensions CreateExtensions(IDictionary objectIdentifier) {
            IDictionary dictionary = new Dictionary<DerObjectIdentifier, X509Extension>();
            foreach (IASN1ObjectIdentifier key in objectIdentifier.Keys) {
                dictionary.Add(((ASN1ObjectIdentifierBC)key).GetASN1ObjectIdentifier(), 
                    ((ExtensionBC)objectIdentifier[key]).GetX509Extension());
            }
            return new ExtensionsBC(new X509Extensions(dictionary));
        }

        public virtual IExtensions CreateExtensions() {
            return ExtensionsBC.GetInstance();
        }

        public virtual IOCSPReqBuilder CreateOCSPReqBuilder() {
            return new OCSPReqBuilderBC(new OcspReqGenerator());
        }

        public virtual ISigPolicyQualifierInfo CreateSigPolicyQualifierInfo(IASN1ObjectIdentifier objectIdentifier
            , IDERIA5String @string) {
            return new SigPolicyQualifierInfoBC(objectIdentifier, @string);
        }

        public virtual IASN1String CreateASN1String(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerStringBase) {
                return new ASN1StringBC((DerStringBase)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1Primitive CreateASN1Primitive(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is Asn1Object) {
                return new ASN1PrimitiveBC((Asn1Object)encodableBC.GetEncodable());
            }
            return null;
        }

        public IOCSPResponse CreateOCSPResponse(byte[] bytes) {
            return new OCSPResponseBC(OcspResponse.GetInstance(new Asn1InputStream(bytes).ReadObject()));
        }

        public virtual IOCSPResponse CreateOCSPResponse() {
            return OCSPResponseBC.GetInstance();
        }

        public virtual IOCSPResponse CreateOCSPResponse(IOCSPResponseStatus respStatus, IResponseBytes responseBytes) {
            return new OCSPResponseBC(respStatus, responseBytes);
        }

        public IOCSPResponse CreateOCSPResponse(int respStatus, object response) {
            if (response == null) {
                return new OCSPResponseBC(new OcspResponse(new OcspResponseStatus(respStatus), null));
            }
            BasicOcspResponse basicResp = null;
            if (response is IBasicOCSPResponse) {
                basicResp = ((BasicOCSPResponseBC)response).GetBasicOCSPResponse();
                if (basicResp == null) {
                    return new OCSPResponseBC(new OcspResponse(new OcspResponseStatus(respStatus), null));
                }
            }
            if (response is BasicOcspResponse) {
                basicResp = (BasicOcspResponse)response;
            }
            if (basicResp == null) {
                throw new OCSPExceptionBC(new OcspException("unknown response object"));
            }
            Asn1OctetString octs;
            try {
                octs = new DerOctetString(((BasicOCSPResponseBC)response).GetEncoded());
            } catch (Exception e) {
                throw new OCSPExceptionBC(new OcspException("can't encode object.", e));
            }
            ResponseBytes rb = new ResponseBytes(OcspObjectIdentifiers.PkixOcspBasic, octs);
            return new OCSPResponseBC(new OcspResponse(new OcspResponseStatus(respStatus), rb));
        }

        public virtual IResponseBytes CreateResponseBytes(IASN1ObjectIdentifier asn1ObjectIdentifier, IDEROctetString
             derOctetString) {
            return new ResponseBytesBC(asn1ObjectIdentifier, derOctetString);
        }

        public virtual IOCSPResponseStatus CreateOCSPResponseStatus(int status) {
            return new OCSPResponseStatusBC(new OcspResponseStatus(status));
        }

        public virtual IOCSPResponseStatus CreateOCSPResponseStatus() {
            return OCSPResponseStatusBC.GetInstance();
        }

        public virtual ICertificateStatus CreateCertificateStatus() {
            return CertificateStatusBC.GetInstance();
        }

        public IRevokedStatus CreateRevokedStatus(ICertificateStatus certificateStatus) {
            CertStatus certStatus = ((CertificateStatusBC) certificateStatus).GetCertificateStatus();
            if (certStatus != null && certStatus.TagNo == 1) {
                return new RevokedStatusBC(certStatus);
            }
            return null;
        }

        public virtual IRevokedStatus CreateRevokedStatus(DateTime date, int i) {
            return new RevokedStatusBC(date, i);
        }

        public virtual IASN1Primitive CreateASN1Primitive(byte[] array) {
            return new ASN1PrimitiveBC(array);
        }

        public virtual IDERIA5String CreateDERIA5String(IASN1TaggedObject taggedObject, bool b) {
            return new DERIA5StringBC(((DerIA5String)DerIA5String.GetInstance(((ASN1TaggedObjectBC)taggedObject).GetASN1TaggedObject
                (), b)));
        }

        public virtual IDERIA5String CreateDERIA5String(String str) {
            return new DERIA5StringBC(str);
        }

        public virtual ICRLDistPoint CreateCRLDistPoint(Object @object) {
            return new CRLDistPointBC(CrlDistPoint.GetInstance(@object is ASN1EncodableBC ? ((ASN1EncodableBC)@object)
                .GetEncodable() : @object));
        }

        public virtual IDistributionPointName CreateDistributionPointName() {
            return DistributionPointNameBC.GetInstance();
        }

        public virtual IGeneralNames CreateGeneralNames(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is GeneralNames) {
                return new GeneralNamesBC((GeneralNames)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IGeneralName CreateGeneralName() {
            return GeneralNameBC.GetInstance();
        }

        public virtual IOtherHashAlgAndValue CreateOtherHashAlgAndValue(IAlgorithmIdentifier algorithmIdentifier, 
            IASN1OctetString octetString) {
            return new OtherHashAlgAndValueBC(algorithmIdentifier, octetString);
        }

        public virtual ISignaturePolicyId CreateSignaturePolicyId(IASN1ObjectIdentifier objectIdentifier, IOtherHashAlgAndValue
             algAndValue) {
            return new SignaturePolicyIdBC(objectIdentifier, algAndValue);
        }

        public virtual ISignaturePolicyId CreateSignaturePolicyId(IASN1ObjectIdentifier objectIdentifier, IOtherHashAlgAndValue
             algAndValue, params ISigPolicyQualifierInfo[] policyQualifiers) {
            SigPolicyQualifierInfo[] qualifierInfos = new SigPolicyQualifierInfo[policyQualifiers.Length];
            for (int i = 0; i < qualifierInfos.Length; ++i) {
                qualifierInfos[i] = ((SigPolicyQualifierInfoBC)policyQualifiers[i]).GetSigPolicyQualifierInfo();
            }
            return new SignaturePolicyIdBC(objectIdentifier, algAndValue, qualifierInfos);
        }

        public virtual ISignaturePolicyIdentifier CreateSignaturePolicyIdentifier(ISignaturePolicyId policyId) {
            return new SignaturePolicyIdentifierBC(policyId);
        }

        public virtual IEnvelopedData CreateEnvelopedData(IOriginatorInfo originatorInfo, IASN1Set set, IEncryptedContentInfo
             encryptedContentInfo, IASN1Set set1) {
            return new EnvelopedDataBC(originatorInfo, set, encryptedContentInfo, set1);
        }

        public virtual IRecipientInfo CreateRecipientInfo(IKeyTransRecipientInfo keyTransRecipientInfo) {
            return new RecipientInfoBC(keyTransRecipientInfo);
        }

        public virtual IEncryptedContentInfo CreateEncryptedContentInfo(IASN1ObjectIdentifier data, IAlgorithmIdentifier
             algorithmIdentifier, IASN1OctetString octetString) {
            return new EncryptedContentInfoBC(data, algorithmIdentifier, octetString);
        }

        public virtual ITBSCertificate CreateTBSCertificate(IASN1Encodable encodable) {
            return new TBSCertificateBC(TbsCertificateStructure.GetInstance(((ASN1EncodableBC)encodable).GetEncodable(
                )));
        }

        public virtual IIssuerAndSerialNumber CreateIssuerAndSerialNumber(IX500Name issuer, IBigInteger value) {
            return new IssuerAndSerialNumberBC(issuer, ((BigIntegerBC)value).GetBigInteger());
        }

        public virtual IRecipientIdentifier CreateRecipientIdentifier(IIssuerAndSerialNumber issuerAndSerialNumber
            ) {
            return new RecipientIdentifierBC(issuerAndSerialNumber);
        }

        public virtual IKeyTransRecipientInfo CreateKeyTransRecipientInfo(IRecipientIdentifier recipientIdentifier
            , IAlgorithmIdentifier algorithmIdentifier, IASN1OctetString octetString) {
            return new KeyTransRecipientInfoBC(recipientIdentifier, algorithmIdentifier, octetString);
        }

        public virtual IOriginatorInfo CreateNullOriginatorInfo() {
            return new OriginatorInfoBC(null);
        }

        public virtual ICMSEnvelopedData CreateCMSEnvelopedData(byte[] bytes) {
            try {
                return new CMSEnvelopedDataBC(new CmsEnvelopedData(bytes));
            }
            catch (CmsException e) {
                throw new CMSExceptionBC(e);
            }
        }

        public virtual ITimeStampRequestGenerator CreateTimeStampRequestGenerator() {
            return new TimeStampRequestGeneratorBC(new TimeStampRequestGenerator());
        }

        public virtual ITimeStampResponse CreateTimeStampResponse(byte[] respBytes) {
            try {
                return new TimeStampResponseBC(new TimeStampResponse(respBytes));
            } catch (TspException e) {
                throw new TSPExceptionBC(e);
            }
        }

        public virtual AbstractOCSPException CreateAbstractOCSPException(Exception e) {
            return new OCSPExceptionBC(new OcspException(e.Message));
        }

        public virtual IUnknownStatus CreateUnknownStatus() {
            return new UnknownStatusBC();
        }

        public virtual IASN1Dump CreateASN1Dump() {
            return ASN1DumpBC.GetInstance();
        }

        public virtual IASN1BitString CreateASN1BitString(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerBitString) {
                return new ASN1BitStringBC((DerBitString)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1GeneralizedTime CreateASN1GeneralizedTime(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerGeneralizedTime) {
                return new ASN1GeneralizedTimeBC((DerGeneralizedTime)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual IASN1UTCTime CreateASN1UTCTime(IASN1Encodable encodable) {
            ASN1EncodableBC encodableBC = (ASN1EncodableBC)encodable;
            if (encodableBC.GetEncodable() is DerUtcTime) {
                return new ASN1UTCTimeBC((DerUtcTime)encodableBC.GetEncodable());
            }
            return null;
        }

        public virtual ITimeStampResponseGenerator CreateTimeStampResponseGenerator(ITimeStampTokenGenerator tokenGenerator
            , IList algorithms) {
            return new TimeStampResponseGeneratorBC(tokenGenerator, algorithms);
        }

        public virtual ITimeStampRequest CreateTimeStampRequest(byte[] bytes) {
            return new TimeStampRequestBC(new TimeStampRequest(bytes));
        }

        public ITimeStampTokenGenerator CreateTimeStampTokenGenerator(IPrivateKey pk, IX509Certificate certificate, 
            string allowedDigest, string policyOid) {
            return new TimeStampTokenGeneratorBC(pk, certificate, allowedDigest, policyOid);
        }

        public virtual IX500Name CreateX500Name(IX509Certificate certificate) {
            byte[] tbsCertificate = certificate.GetTbsCertificate();
            if (tbsCertificate.Length != 0) {
                return new X500NameBC(X509Name.GetInstance(TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(
                    certificate.GetTbsCertificate())).Subject));
            }
            return null;
        }

        public virtual IX500Name CreateX500Name(String s) {
            return new X500NameBC(new X509Name(s));
        }

        public virtual IRespID CreateRespID(IX500Name x500Name) {
            return new RespIDBC(x500Name);
        }

        public virtual IBasicOCSPRespBuilder CreateBasicOCSPRespBuilder(IRespID respID) {
            return new BasicOCSPRespBuilderBC(respID);
        }

        public virtual IOCSPReq CreateOCSPReq(byte[] requestBytes) {
            return new OCSPReqBC(new OcspReq(requestBytes));
        }

        public virtual IX509v2CRLBuilder CreateX509v2CRLBuilder(IX500Name x500Name, DateTime date) {
            return new X509v2CRLBuilderBC(x500Name, date);
        }

        public virtual IJcaX509v3CertificateBuilder CreateJcaX509v3CertificateBuilder(IX509Certificate signingCert, 
            IBigInteger number, DateTime startDate, DateTime endDate, IX500Name subjectDnName, IPublicKey publicKey) {
            return new JcaX509v3CertificateBuilderBC(signingCert, number, startDate, endDate, subjectDnName, publicKey);
        }

        public virtual IBasicConstraints CreateBasicConstraints(bool b) {
            return new BasicConstraintsBC(new BasicConstraints(b));
        }

        public virtual IKeyUsage CreateKeyUsage() {
            return KeyUsageBC.GetInstance();
        }

        public virtual IKeyUsage CreateKeyUsage(int i) {
            return new KeyUsageBC(new KeyUsage(i));
        }

        public virtual IKeyPurposeId CreateKeyPurposeId() {
            return KeyPurposeIdBC.GetInstance();
        }

        public virtual IExtendedKeyUsage CreateExtendedKeyUsage(IKeyPurposeId purposeId) {
            return new ExtendedKeyUsageBC(purposeId);
        }
        
        public virtual ISubjectPublicKeyInfo CreateSubjectPublicKeyInfo(IPublicKey publicKey) {
            return new SubjectPublicKeyInfoBC(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(
                ((PublicKeyBC)publicKey).GetPublicKey()));
        }

        public virtual ICRLReason CreateCRLReason() {
            return CRLReasonBC.GetInstance();
        }

        public virtual ITSTInfo CreateTSTInfo(IContentInfo contentInfoTsp) {
            CmsProcessable content = new CmsSignedData(((ContentInfoBC) contentInfoTsp).GetContentInfo()).SignedContent;
            MemoryStream bOut = new MemoryStream();
            content.Write(bOut);
            return new TSTInfoBC(TstInfo.GetInstance(Asn1Object.FromByteArray(bOut.ToArray())));
        }

        public virtual ISingleResp CreateSingleResp(IBasicOCSPResponse basicResp) {
            return new SingleRespBC(basicResp);
        }

        public virtual IX509Certificate CreateX509Certificate(object obj) {
            switch (obj) {
                case IX509Certificate _:
                    return (X509CertificateBC) obj;
                case X509Certificate certificate:
                    return new X509CertificateBC(certificate);
                case byte[] encoded:
                    return new X509CertificateBC(new X509CertificateParser().ReadCertificate(encoded));
                default:
                    return null;
            }
        }
        
        public virtual IX509Certificate CreateX509Certificate(Stream s) {
            return new X509CertificateBC(new X509CertificateParser().ReadCertificate(s));
        }
        
        public IX509Crl CreateX509Crl(Stream input) {
            return new X509CrlBC(new X509CrlParser().ReadCrl(input));
        }

        public IIDigest CreateIDigest(string hashAlgorithm) {
            return new IDigestBC(DigestUtilities.GetDigest(hashAlgorithm));
        }

        public ICertificateID CreateCertificateID(string hashAlgorithm, IX509Certificate issuerCert, IBigInteger serialNumber) {
            return new CertificateIDBC(hashAlgorithm, issuerCert, serialNumber);
        }
        
        public IX500Name CreateX500NameInstance(IASN1Encodable issuer) {
            return new X500NameBC(X509Name.GetInstance(((ASN1EncodableBC)issuer).GetEncodable()));
        }

        public IOCSPReq CreateOCSPReq(ICertificateID certId, byte[] documentId) {
            return new OCSPReqBC(certId, documentId);
        }
        
        public IISigner CreateISigner() {
            return new ISignerBC(null);
        }
        
        public IX509CertificateParser CreateX509CertificateParser() {
            return new X509CertificateParserBC(new X509CertificateParser());
        }

        public AbstractGeneralSecurityException CreateGeneralSecurityException(string exceptionMessage,
            Exception exception) {
            return new GeneralSecurityExceptionBC(exceptionMessage, exception);
        }
        
        public AbstractGeneralSecurityException CreateGeneralSecurityException(string exceptionMessage) {
            return new GeneralSecurityExceptionBC(new GeneralSecurityException(exceptionMessage));
        }
        
        public AbstractGeneralSecurityException CreateGeneralSecurityException() {
            return new GeneralSecurityExceptionBC(new GeneralSecurityException());
        }

    	public IBouncyCastleTestConstantsFactory GetBouncyCastleFactoryTestUtil() {
        	return BOUNCY_CASTLE_TEST_CONSTANTS;
    	}

        public IBigInteger CreateBigInteger() {
            return BigIntegerBC.GetInstance();
        }
        
        public IBigInteger CreateBigInteger(int i, byte[] array) {
            return new BigIntegerBC(new BigInteger(i, array));
        }

        public IBigInteger CreateBigInteger(string str) {
            return new BigIntegerBC(new BigInteger(str));
        }

        public ICipher CreateCipher(bool forEncryption, byte[] key, byte[] iv) {
            return new CipherBC(forEncryption, key, iv);
        }

        public ICipherCBCnoPad CreateCipherCbCnoPad(bool forEncryption, byte[] key, byte[] iv) {
            return new CipherCBCnoPadBC(forEncryption, key, iv);
        }
        
        public ICipherCBCnoPad CreateCipherCbCnoPad(bool forEncryption, byte[] key) {
            return new CipherCBCnoPadBC(forEncryption, key);
        }
        
        public IX509Crl CreateNullCrl() {
            return new X509CrlBC(null);
        }

        public ITimeStampToken CreateTimeStampToken(IContentInfo contentInfo) {
            return new TimeStampTokenBC(new TimeStampToken(((ContentInfoBC)contentInfo).GetContentInfo()));
        }

        public IRsaKeyPairGenerator CreateRsa2048KeyPairGenerator() {
            return new RsaKeyPairGeneratorBC();
        }

        public IContentSigner CreateContentSigner(string signatureAlgorithm, IPrivateKey signingKey) {
            return new ContentSignerBC(new Asn1SignatureFactory(signatureAlgorithm, 
                (AsymmetricKeyParameter)((PrivateKeyBC) signingKey).GetPrivateKey()));
        }

        public IAuthorityKeyIdentifier CreateAuthorityKeyIdentifier(ISubjectPublicKeyInfo issuerPublicKeyInfo) {
            return new AuthorityKeyIdentifierBC(new AuthorityKeyIdentifier(
                ((SubjectPublicKeyInfoBC)issuerPublicKeyInfo).GetSubjectPublicKeyInfo()));
        }

        public ISubjectKeyIdentifier CreateSubjectKeyIdentifier(ISubjectPublicKeyInfo subjectPublicKeyInfo) {
            return new SubjectKeyIdentifierBC(new SubjectKeyIdentifier(
                ((SubjectPublicKeyInfoBC)subjectPublicKeyInfo).GetSubjectPublicKeyInfo()));
        }

        public bool IsNullExtension(IExtension ext) {
            return ((ExtensionBC)ext).GetX509Extension() == null;
        }
        
        public IExtension CreateExtension(bool b, IDEROctetString octetString) {
            return new ExtensionBC(new X509Extension(b, 
                ((DEROctetStringBC)octetString).GetDEROctetString()));
        }

        public byte[] CreateCipherBytes(IX509Certificate x509Certificate, byte[] abyte0, IAlgorithmIdentifier algorithmidentifier) {
            IBufferedCipher cipher = CipherUtilities.GetCipher(((AlgorithmIdentifierBC) algorithmidentifier).GetAlgorithmIdentifier().Algorithm);
            cipher.Init(true, ((PublicKeyBC)x509Certificate.GetPublicKey()).GetPublicKey());
            byte[] outp = new byte[10000];
            int len = cipher.DoFinal(abyte0, outp, 0);
            byte[] abyte1 = new byte[len];
            Array.Copy(outp, 0, abyte1, 0, len);
            return abyte1;
        }

        public IPEMParser CreatePEMParser(TextReader reader, char[] password) {
            return new PEMParserBC(new PemReader(reader, new BouncyCastlePasswordFinder(password)));
        }

        private class BouncyCastlePasswordFinder : IPasswordFinder {
            private readonly char[] password;

            public BouncyCastlePasswordFinder(char[] password) {
                this.password = password;
            }
            
            public char[] GetPassword() {
                return password;
            }
        }
    }
}